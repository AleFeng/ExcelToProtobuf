using System;
using System.IO;

namespace ExcelToProtobuf
{
    /// <summary>
    /// 转换管线的输入/输出路径配置。
    /// 默认沿用仓库布局（向后兼容），也可通过命令行参数覆盖任意路径。
    /// </summary>
    public class PipelineConfig
    {
        public string ExcelDir;             // 输入：Excel 配置表目录
        public string HandwrittenProtoDir;  // 输入：手写 .proto 目录（可选）
        public string ProtoOutDir;          // 中间：生成的 .proto
        public string CsGenDir;             // 中间：protoc 生成的 .cs
        public string UnityCsDir;           // 输出：拷贝到 Unity 的配置类 .cs
        public string BytesOutDir;          // 输出：序列化 .bytes
        public string TxtOutDir;            // 输出：明文 .txt

        public static PipelineConfig CreateDefault()
        {
            string root = LocateRepoRoot();
            string work = Path.Combine(Path.GetTempPath(), "ExcelToProtobuf");
            return new PipelineConfig
            {
                ExcelDir = Path.Combine(root, "Config", "Excel"),
                HandwrittenProtoDir = Path.Combine(root, "Config", "Proto"),
                ProtoOutDir = Path.Combine(work, "Protos"),
                CsGenDir = Path.Combine(work, "Csharp"),
                UnityCsDir = Path.Combine(root, "Assets", "Source", "System", "ConfigSystem", "Config"),
                BytesOutDir = Path.Combine(root, "Assets", "ProductAssets", "Config"),
                TxtOutDir = Path.Combine(root, "Assets", "UnProductAssets", "Config"),
            };
        }

        /// <summary>从可执行文件位置向上查找包含 Config/Excel 的仓库根目录。</summary>
        private static string LocateRepoRoot()
        {
            DirectoryInfo dir = new DirectoryInfo(AppContext.BaseDirectory);
            while (dir != null)
            {
                if (Directory.Exists(Path.Combine(dir.FullName, "Config", "Excel")))
                    return dir.FullName;
                dir = dir.Parent;
            }
            return Directory.GetCurrentDirectory(); // 回退
        }

        /// <summary>用命令行参数（--key value）覆盖默认路径。</summary>
        public void ApplyArgs(string[] args)
        {
            if (args == null) return;
            for (int i = 0; i + 1 < args.Length; i += 2)
            {
                string key = args[i];
                string val = args[i + 1];
                switch (key)
                {
                    case "--excel": ExcelDir = val; break;
                    case "--proto-src": HandwrittenProtoDir = val; break;
                    case "--proto-out": ProtoOutDir = val; break;
                    case "--cs-gen": CsGenDir = val; break;
                    case "--cs-out": UnityCsDir = val; break;
                    case "--bytes-out": BytesOutDir = val; break;
                    case "--txt-out": TxtOutDir = val; break;
                }
            }
        }

        public void Print()
        {
            Console.WriteLine("路径配置：");
            Console.WriteLine($"  Excel 输入      : {ExcelDir}");
            Console.WriteLine($"  手写 proto      : {HandwrittenProtoDir}");
            Console.WriteLine($"  proto 生成(中间) : {ProtoOutDir}");
            Console.WriteLine($"  cs 生成(中间)    : {CsGenDir}");
            Console.WriteLine($"  Unity cs 输出   : {UnityCsDir}");
            Console.WriteLine($"  bytes 输出      : {BytesOutDir}");
            Console.WriteLine($"  txt 输出        : {TxtOutDir}");
            Console.WriteLine();
        }
    }
}
