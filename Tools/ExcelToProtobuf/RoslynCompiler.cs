using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace ExcelToProtobuf
{
    // .cs文件转内存程序集：用 Roslyn 在进程内编译（取代 BuildDLL.bat 与硬编码的 csc.exe）
    public static class RoslynCompiler
    {
        public static Assembly CompileInMemory(string csDir, out string error)
        {
            error = null;

            if (!Directory.Exists(csDir))
            {
                error = $"需要编译的C#文件夹不存在-{csDir}";
                return null;
            }

            string[] files = Directory.GetFiles(csDir, "*.cs", SearchOption.AllDirectories);
            if (files.Length == 0)
            {
                error = $"未在 {csDir} 找到 .cs 文件";
                return null;
            }

            var trees = files
                .Select(f => CSharpSyntaxTree.ParseText(File.ReadAllText(f), path: f))
                .ToList();

            var refs = new List<MetadataReference>();

            // 运行时可信平台程序集（netstandard、System.* 等）
            string tpa = AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES") as string;
            if (!string.IsNullOrEmpty(tpa))
            {
                foreach (var path in tpa.Split(Path.PathSeparator))
                {
                    if (path.EndsWith(".dll", StringComparison.OrdinalIgnoreCase) && File.Exists(path))
                        refs.Add(MetadataReference.CreateFromFile(path));
                }
            }

            // Google.Protobuf（NuGet 依赖，不在 TPA 中，需显式加入）
            refs.Add(MetadataReference.CreateFromFile(typeof(Google.Protobuf.IMessage).Assembly.Location));

            var compilation = CSharpCompilation.Create(
                "ConfigProto",
                trees,
                refs,
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, optimizationLevel: OptimizationLevel.Release));

            using var ms = new MemoryStream();
            var result = compilation.Emit(ms);
            if (!result.Success)
            {
                var errs = result.Diagnostics
                    .Where(d => d.Severity == DiagnosticSeverity.Error)
                    .Take(20)
                    .Select(d => d.ToString());
                error = "C# 编译失败：\n" + string.Join("\n", errs);
                return null;
            }

            ms.Seek(0, SeekOrigin.Begin);
            return Assembly.Load(ms.ToArray());
        }
    }
}
