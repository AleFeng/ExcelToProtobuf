using System;
using System.Collections.Generic;
using System.Reflection;
using Google.Protobuf;

namespace ExcelToProtobuf
{
    /// <summary>
    /// 配置表运行时加载器：按表名加载 .bytes，反序列化为工具生成的 Protobuf 容器类
    /// （<c>&lt;SheetName&gt;_Map</c>），并按类型缓存，避免重复解析。
    /// </summary>
    /// <example>
    /// <code>
    /// using Deploy;             // 生成的配置类命名空间
    /// using ExcelToProtobuf;    // 本加载器
    ///
    /// var cfg = new ConfigManager();                       // 默认从 Resources/Config 加载
    /// Common_Quality_Map map = cfg.Load&lt;Common_Quality_Map&gt;();
    /// Common_Quality row = map.Items[1];                   // 按主键取一行
    /// </code>
    /// </example>
    public class ConfigManager
    {
        private readonly IConfigBytesProvider _provider;
        private readonly Dictionary<Type, object> _cache = new Dictionary<Type, object>();

        /// <param name="provider">字节来源，默认 <see cref="ResourcesConfigBytesProvider"/>（Resources/Config）。</param>
        public ConfigManager(IConfigBytesProvider provider = null)
        {
            _provider = provider ?? new ResourcesConfigBytesProvider();
        }

        /// <summary>
        /// 加载并缓存整张表的容器类。<typeparamref name="TMap"/> 为工具生成的 "&lt;Sheet&gt;_Map" 类型。
        /// </summary>
        /// <param name="tableName">
        /// .bytes 的表名；为空时自动取 <typeparamref name="TMap"/> 类型名去掉 "_Map" 后缀。
        /// </param>
        public TMap Load<TMap>(string tableName = null) where TMap : class, IMessage<TMap>, new()
        {
            Type t = typeof(TMap);
            if (_cache.TryGetValue(t, out object cached))
                return (TMap)cached;

            if (string.IsNullOrEmpty(tableName))
                tableName = ResolveTableName(t);

            byte[] bytes = _provider.Load(tableName);
            if (bytes == null)
                throw new InvalidOperationException($"[ConfigManager] 未找到配置数据：{tableName}");

            MessageParser<TMap> parser = ResolveParser<TMap>();
            TMap map = parser.ParseFrom(bytes);
            _cache[t] = map;
            return map;
        }

        /// <summary>获取已缓存的表；未加载则返回 false（不会触发加载）。</summary>
        public bool TryGetCached<TMap>(out TMap map) where TMap : class, IMessage<TMap>, new()
        {
            if (_cache.TryGetValue(typeof(TMap), out object cached))
            {
                map = (TMap)cached;
                return true;
            }
            map = null;
            return false;
        }

        /// <summary>清空缓存（例如热更新配置后）。</summary>
        public void Clear() => _cache.Clear();

        // "<Sheet>_Map" -> "<Sheet>"
        private static string ResolveTableName(Type mapType)
        {
            const string suffix = "_Map";
            string n = mapType.Name;
            return n.EndsWith(suffix) ? n.Substring(0, n.Length - suffix.Length) : n;
        }

        // 取生成类型的静态 Parser 属性（protobuf 生成类均有 public static MessageParser<T> Parser）
        private static MessageParser<TMap> ResolveParser<TMap>() where TMap : class, IMessage<TMap>, new()
        {
            PropertyInfo p = typeof(TMap).GetProperty("Parser", BindingFlags.Public | BindingFlags.Static);
            if (p?.GetValue(null) is MessageParser<TMap> parser)
                return parser;
            throw new InvalidOperationException($"[ConfigManager] 类型 {typeof(TMap).Name} 缺少静态 Parser 属性");
        }
    }
}
