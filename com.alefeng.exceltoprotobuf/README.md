# Excel To Protobuf (Unity 运行时包)

将 Excel 配置表转换为 Protobuf 的 C# 类与二进制数据。本 UPM 包提供 **Unity 运行时**部分：

- `ConfigManager` —— 按表名加载 `.bytes` 并反序列化为强类型 Protobuf 容器类，带缓存。
- `IConfigBytesProvider` / `ResourcesConfigBytesProvider` —— 可替换的字节来源（默认从 `Resources` 加载）。
- 随包内置 `Google.Protobuf` 运行时依赖。

> 负责「Excel → proto → C# 类 → .bytes」的**转换器**是一个独立的 .NET 命令行工具，在 Unity 外运行。
> 完整说明（含填表规则、转换器用法、多语言文档）见仓库根目录：
> [中文](https://github.com/AleFeng/ExcelToProtobuf/blob/main/README.md) ·
> [English](https://github.com/AleFeng/ExcelToProtobuf/blob/main/README_EN.md) ·
> [日本語](https://github.com/AleFeng/ExcelToProtobuf/blob/main/README_JA.md)

## 安装（UPM，git URL）
`Window → Package Manager → + → Install package from git URL...`，粘贴：

```
https://github.com/AleFeng/ExcelToProtobuf.git?path=com.alefeng.exceltoprotobuf
```

或在 `Packages/manifest.json` 的 `dependencies` 中加入：

```json
"com.alefeng.exceltoprotobuf": "https://github.com/AleFeng/ExcelToProtobuf.git?path=com.alefeng.exceltoprotobuf"
```

## 快速使用
1. 用转换器把 Excel 导出为 `.bytes`，放到项目的 `Resources/Config/` 下（例如 `Resources/Config/Common_Quality.bytes`）。
2. 把转换器生成的配置类 `.cs`（`Deploy` 命名空间）加入你的工程。
3. 运行时加载：

```csharp
using Deploy;            // 生成的配置类
using ExcelToProtobuf;   // 本包

var config = new ConfigManager();                        // 默认 Resources/Config
Common_Quality_Map map = config.Load<Common_Quality_Map>();
Common_Quality row = map.Items[1];                       // 按主键取一行
```

自定义字节来源（如 Addressables / StreamingAssets）：实现 `IConfigBytesProvider` 并传入 `new ConfigManager(myProvider)`。

## 示例
在 Package Manager 中选中本包 → `Samples` → 导入 **Basic Usage**，把 `ConfigDemo` 挂到场景物体上运行即可看到日志输出。

## 许可
MIT（见 `LICENSE.md`）。
