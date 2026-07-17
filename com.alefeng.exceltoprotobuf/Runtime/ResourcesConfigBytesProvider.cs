using UnityEngine;

namespace ExcelToProtobuf
{
    /// <summary>
    /// 默认的字节来源实现：从 <c>Resources</c> 加载 .bytes。
    /// 约定配置数据放在 <c>Resources/&lt;root&gt;/&lt;tableName&gt;.bytes</c>（.bytes 会被识别为 TextAsset）。
    /// </summary>
    public class ResourcesConfigBytesProvider : IConfigBytesProvider
    {
        private readonly string _root;

        /// <param name="resourcesRoot">Resources 下的相对子目录，默认 "Config"。</param>
        public ResourcesConfigBytesProvider(string resourcesRoot = "Config")
        {
            _root = string.IsNullOrEmpty(resourcesRoot) ? "" : resourcesRoot.TrimEnd('/') + "/";
        }

        public byte[] Load(string tableName)
        {
            TextAsset asset = Resources.Load<TextAsset>(_root + tableName);
            return asset != null ? asset.bytes : null;
        }
    }
}
