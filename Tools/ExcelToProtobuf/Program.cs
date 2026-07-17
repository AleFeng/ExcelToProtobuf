using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;

namespace ExcelToProtobuf
{
    class Program
    {
        static int Main(string[] args)
        {
            var cfg = PipelineConfig.CreateDefault();
            cfg.ApplyArgs(args);
            cfg.Print();

            if (!Directory.Exists(cfg.ExcelDir))
            {
                Error($"源配置文件夹不存在：{cfg.ExcelDir}（可用 --excel <目录> 指定）");
                return Exit(1);
            }

            // 1. Excel -> proto
            Console.WriteLine("【Excel转Proto】开始...");
            Excel2Proto.Compiler(cfg.ExcelDir, cfg.ProtoOutDir);
            Console.WriteLine("【Excel转Proto】结束...\n");

            // 2. 拷贝手写 proto
            Console.WriteLine("【Proto拷贝】开始...");
            CopyHandwrittenProtos(cfg.HandwrittenProtoDir, cfg.ProtoOutDir);
            Console.WriteLine("【Proto拷贝】结束...\n");

            // 3. proto -> cs（protoc）
            Console.WriteLine("【Proto转C#】开始...");
            if (!ProtocRunner.Run(cfg.ProtoOutDir, cfg.CsGenDir, out string protoErr))
            {
                Error(protoErr);
                return Exit(1);
            }
            Console.WriteLine("【Proto转C#】结束...\n");

            // 4. 拷贝 cs 到 Unity（哈希比对 + 清理废弃）
            Console.WriteLine("【C#拷贝至Unity】开始...");
            CopyCs2Unity(cfg.CsGenDir, cfg.UnityCsDir);
            Console.WriteLine("【C#拷贝至Unity】结束...\n");

            // 5. Roslyn 内存编译 cs -> 程序集
            Console.WriteLine("【C#编译(内存)】开始...");
            Assembly configAsm = RoslynCompiler.CompileInMemory(cfg.CsGenDir, out string compErr);
            if (configAsm == null)
            {
                Error(compErr);
                return Exit(1);
            }
            Console.WriteLine("【C#编译(内存)】结束...\n");

            // 6. Excel -> bytes + txt
            Console.WriteLine("【序列化保存配置表数据】开始...");
            Excel2Bytes.Compiler(configAsm, cfg.ExcelDir, cfg.BytesOutDir, cfg.TxtOutDir);
            Success("【序列化保存配置表数据】结束...\n");

            Success("流程执行完毕。");
            return Exit(0);
        }

        // 拷贝手写 proto（Config/Proto/*.proto）到 protoc 的输入目录
        private static void CopyHandwrittenProtos(string srcDir, string destDir)
        {
            if (!Directory.Exists(srcDir))
            {
                Console.WriteLine($">> 跳过 >> 手写 proto 目录不存在-{srcDir}");
                return;
            }

            Directory.CreateDirectory(destDir);

            FileInfo[] files = new DirectoryInfo(srcDir).GetFiles();
            foreach (var file in files)
            {
                if (file.Name.EndsWith("proto"))
                {
                    file.CopyTo(Path.Combine(destDir, file.Name), true);
                    Console.WriteLine($">> 拷贝完成 >> {file.Name}");
                }
            }
        }

        // 拷贝生成的 C# 到 Unity（哈希比对：相同跳过、变化覆盖、废弃删除）
        private static void CopyCs2Unity(string srcDir, string destDir)
        {
            if (!Directory.Exists(srcDir))
            {
                Console.WriteLine($">> 拷贝失败 >> 文件夹不存在-{srcDir}");
                return;
            }

            Directory.CreateDirectory(destDir);

            //遍历旧C#文件
            DirectoryInfo destDirInfo = new DirectoryInfo(destDir);
            FileInfo[] destFiles = destDirInfo.GetFiles();
            Dictionary<string, FileInfo> filesMap = new Dictionary<string, FileInfo>();
            foreach (var file in destFiles)
            {
                filesMap[file.Name] = file;
            }

            //拷贝新C#文件
            using MD5 hash = MD5.Create();

            DirectoryInfo srcDirInfo = new DirectoryInfo(srcDir);
            FileInfo[] srcFiles = srcDirInfo.GetFiles();
            foreach (var newFile in srcFiles)
            {
                string newfileName = newFile.Name;
                if (!newfileName.EndsWith(".cs")) { continue; } // 跳过非 .cs（此前误用 return 会中止整个流程）

                //判断新旧文件是否相同
                bool isSame = false;
                if (filesMap.TryGetValue(newfileName, out FileInfo oldFile))
                {
                    using (FileStream oldFs = new FileStream(oldFile.FullName, FileMode.Open), newFs = new FileStream(newFile.FullName, FileMode.Open))
                    {
                        string oldHashStr = BitConverter.ToString(hash.ComputeHash(oldFs));
                        string newHashStr = BitConverter.ToString(hash.ComputeHash(newFs));
                        if (oldHashStr.Equals(newHashStr))
                        {
                            isSame = true;
                        }
                    }
                }

                if (isSame)
                {
                    Console.WriteLine($">> 文件相同 >> {newfileName}");
                }
                else
                {
                    newFile.CopyTo(Path.Combine(destDir, newfileName), true);
                    Console.WriteLine($">> 拷贝完成 >> {newfileName}");
                }

                filesMap.Remove(newfileName); //已处理文件从记录中移除
            }

            //删除多余或弃用的配置表C#
            foreach (var fileInfo in filesMap.Values)
            {
                string fileName = fileInfo.Name;
                if (fileName.EndsWith(".meta")) { continue; }

                fileInfo.Delete();
                Console.WriteLine($">> 删除弃用 >> {fileName}");
            }
        }

        // ---- 输出辅助 ----
        private static void Success(string msg) => WriteColor(msg, ConsoleColor.Green);

        private static void Error(string msg) => WriteColor(">> 失败 >> " + msg, ConsoleColor.Red);

        private static void WriteColor(string msg, ConsoleColor color)
        {
            ConsoleColor old = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(msg);
            Console.ForegroundColor = old;
        }

        // 交互式运行时等待按键；被重定向（脚本/CI）时直接退出，避免阻塞
        private static int Exit(int code)
        {
            if (!Console.IsInputRedirected)
            {
                Console.WriteLine("\n按任意键退出");
                try { Console.ReadKey(); } catch { /* 无可用控制台输入时忽略 */ }
            }
            return code;
        }
    }
}
