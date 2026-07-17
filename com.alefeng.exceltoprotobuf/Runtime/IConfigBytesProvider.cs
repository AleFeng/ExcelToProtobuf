namespace ExcelToProtobuf
{
    /// <summary>
    /// 抽象「表名 -> .bytes 字节」的来源，便于适配不同的资源加载方式
    /// （Resources / Addressables / StreamingAssets / AssetBundle 等）。
    /// </summary>
    public interface IConfigBytesProvider
    {
        /// <summary>返回指定表名的序列化字节；找不到时返回 null。</summary>
        byte[] Load(string tableName);
    }
}
