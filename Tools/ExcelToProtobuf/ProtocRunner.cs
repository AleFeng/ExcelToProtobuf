using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace ExcelToProtobuf
{
    // .proto文件转class：直接调用 Google.Protobuf.Tools 附带的 protoc（取代 BuildProtos.bat）
    public static class ProtocRunner
    {
        public static bool Run(string protoDir, string csOutDir, out string error)
        {
            error = null;

            string protoc = LocateProtoc();
            if (protoc == null)
            {
                error = "未找到 protoc（应位于输出目录 protoc_tools/<rid>/ 下）。请先执行 dotnet build 以还原 Google.Protobuf.Tools。";
                return false;
            }

            if (!Directory.Exists(protoDir))
            {
                error = $"proto 源目录不存在-{protoDir}";
                return false;
            }

            Directory.CreateDirectory(csOutDir);
            string[] protoFiles = Directory.GetFiles(protoDir, "*.proto", SearchOption.AllDirectories);
            if (protoFiles.Length == 0)
            {
                error = $"未在 {protoDir} 找到 .proto 文件";
                return false;
            }

            var psi = new ProcessStartInfo
            {
                FileName = protoc,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };
            psi.ArgumentList.Add($"-I={protoDir}");
            psi.ArgumentList.Add($"--csharp_out={csOutDir}");
            foreach (var f in protoFiles) psi.ArgumentList.Add(f);

            try
            {
                using var proc = Process.Start(psi);
                string stdErr = proc.StandardError.ReadToEnd();
                proc.WaitForExit();
                if (proc.ExitCode != 0)
                {
                    error = $"protoc 退出码 {proc.ExitCode}：{stdErr}";
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                error = $"protoc 调用异常：{e}";
                return false;
            }
        }

        // 依据当前操作系统/架构，在 protoc_tools 下定位 protoc 可执行文件
        private static string LocateProtoc()
        {
            string rid;
            string exe = "protoc";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                rid = RuntimeInformation.OSArchitecture == Architecture.X86 ? "windows_x86" : "windows_x64";
                exe = "protoc.exe";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                rid = "macosx_x64";
            }
            else
            {
                rid = RuntimeInformation.OSArchitecture == Architecture.X86 ? "linux_x86" : "linux_x64";
            }

            string toolsRoot = Path.Combine(AppContext.BaseDirectory, "protoc_tools");
            string expected = Path.Combine(toolsRoot, rid, exe);
            if (File.Exists(expected)) return expected;

            // 兜底：递归查找第一个匹配的 protoc（应对包内子目录命名差异）
            if (Directory.Exists(toolsRoot))
            {
                foreach (var f in Directory.GetFiles(toolsRoot, exe, SearchOption.AllDirectories))
                    return f;
            }
            return null;
        }
    }
}
