using UnityEngine;
using Deploy;            // 工具生成的配置类命名空间
using ExcelToProtobuf;   // 运行时加载器

namespace ExcelToProtobufSamples
{
    /// <summary>
    /// 最小示例：用 <see cref="ConfigManager"/> 从 Resources 加载 Common_Quality 配置并打印。
    /// 把本组件挂到场景中任意 GameObject 上运行即可。
    /// </summary>
    public class ConfigDemo : MonoBehaviour
    {
        private void Start()
        {
            // 默认从 Resources/Config/<表名>.bytes 加载
            var config = new ConfigManager();

            // 加载整张表（容器类 <Sheet>_Map）
            Common_Quality_Map map = config.Load<Common_Quality_Map>();
            Debug.Log($"[ExcelToProtobuf] 载入 Common_Quality，共 {map.Items.Count} 条：");
            foreach (var kv in map.Items)
            {
                Common_Quality q = kv.Value;
                Debug.Log($"  #{q.Id}  {q.Name}  #{q.ColorHex}");
            }

            // 也可以按主键 Id 直接取某一行
            if (map.Items.TryGetValue(5, out Common_Quality rare))
                Debug.Log($"[ExcelToProtobuf] 主键 5 = {rare.Name}");
        }
    }
}
