# Changelog

本文件记录该 UPM 包的版本变更，遵循 [语义化版本](https://semver.org/lang/zh-CN/)。

## [1.0.0] - 2026-07-17
### Added
- 首个 UPM 版本。
- Unity 运行时加载器 `ConfigManager`：按表名加载 `.bytes`，反序列化为强类型 Protobuf 容器类（`<Sheet>_Map`），并按类型缓存。
- 字节来源抽象 `IConfigBytesProvider` 与默认实现 `ResourcesConfigBytesProvider`（从 `Resources` 加载）。
- 内置 `Google.Protobuf`（netstandard2.0）运行时依赖，随包提供。
- `Basic Usage` 示例：加载示例配置并逐行打印。
