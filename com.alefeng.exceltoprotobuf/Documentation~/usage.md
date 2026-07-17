# Excel To Protobuf — 运行时使用说明

本文档介绍 UPM 包**运行时**部分的用法。Excel 填表规则与转换器（.NET 工具）用法见仓库根目录 README。

## 组成
| 类型 | 说明 |
| --- | --- |
| `ConfigManager` | 配置表加载器。按表名加载 `.bytes`，反序列化为 `<Sheet>_Map` 容器类并缓存。 |
| `IConfigBytesProvider` | 字节来源抽象：`byte[] Load(string tableName)`。 |
| `ResourcesConfigBytesProvider` | 默认实现，从 `Resources/<root>/<tableName>.bytes` 加载（`root` 默认 `Config`）。 |

生成的配置类位于 `Deploy` 命名空间；每张表对应数据类 `<Sheet>` 与容器类 `<Sheet>_Map`（内部 `Items` 是以主键 `Id` 为键的字典）。

## 典型用法
```csharp
using Deploy;
using ExcelToProtobuf;

public class Configs
{
    public static readonly ConfigManager Manager = new ConfigManager();

    public static Common_Quality Get(int id)
        => Manager.Load<Common_Quality_Map>().Items[id];
}
```

- `Load<TMap>()`：表名默认取 `TMap` 类型名去掉 `_Map` 后缀（`Common_Quality_Map` → `Common_Quality`）。
- `Load<TMap>("自定义表名")`：显式指定 `.bytes` 文件名。
- `TryGetCached<TMap>(out map)`：只取缓存、不触发加载。
- `Clear()`：清空缓存（热更新配置后调用）。

## 更换资源加载方式
默认从 `Resources` 加载。若使用 Addressables / StreamingAssets / AssetBundle，实现 `IConfigBytesProvider`：

```csharp
public class AddressablesConfigBytesProvider : IConfigBytesProvider
{
    public byte[] Load(string tableName)
    {
        // 例：同步等待一个 Addressables TextAsset 句柄，返回 asset.bytes
        // 具体实现取决于你的资源管线
        ...
    }
}

// 使用
var config = new ConfigManager(new AddressablesConfigBytesProvider());
```

## 依赖
- 随包内置 `Google.Protobuf.dll`（netstandard2.0）。若你的工程已通过其他途径引入了 `Google.Protobuf`，请避免重复引入以防程序集冲突。
- 最低 Unity 版本：`2021.3`（`.NET Standard 2.0`/`2.1` API 兼容级别）。
