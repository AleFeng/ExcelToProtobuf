using Google.Protobuf;
using Google.Protobuf.Collections;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Linq;

namespace ExcelToProtobuf
{
    // excel数据转二进制数据
    public class Excel2Bytes
    {
        private static string m_ExcelFilePath = string.Empty;
        private static string m_CfgBytesPathDir = string.Empty;
        private static string m_CfgTxtPathDir = string.Empty;
        private static Assembly m_ConfigDllAssembly;

        // 数值解析统一使用不变区域，避免受机器区域设置（小数点/千位分隔符）影响
        private static readonly CultureInfo Inv = CultureInfo.InvariantCulture;
        private static readonly Encoding Utf8NoBom = new UTF8Encoding(false);

        // 直接接收已编译好的配置程序集（由 Roslyn 在内存中编译，取代旧的 ConfigProto.dll）
        public static void Compiler(Assembly configAssembly, string srcExcelPathDir, string destCfgBytesPathDir, string destCfgTxtPathDir)
        {
            if (configAssembly == null)
            {
                Console.WriteLine($">> 转换失败 >> 配置程序集为空");
                return;
            }

            if (!Directory.Exists(srcExcelPathDir))
            {
                Console.WriteLine($">> 转换失败 >> 源Excel配置文件夹路径不存在-{srcExcelPathDir}");
                return;
            }

            Directory.CreateDirectory(destCfgBytesPathDir);
            Directory.CreateDirectory(destCfgTxtPathDir);

            m_ConfigDllAssembly = configAssembly;
            m_CfgBytesPathDir = destCfgBytesPathDir;
            m_CfgTxtPathDir = destCfgTxtPathDir;

            //清空旧Config二进制文件
            ClearDir(m_CfgBytesPathDir);
            //清空旧Config文本文件
            ClearDir(m_CfgTxtPathDir);

            string[] excelFilePaths = Directory.GetFiles(srcExcelPathDir, "*.xlsx", SearchOption.AllDirectories);
            for (int i = 0; i < excelFilePaths.Length; i++)
            {
                OpenExcel(excelFilePaths[i]);
            }
        }

        private static void ClearDir(string dir)
        {
            if (!Directory.Exists(dir)) return;
            foreach (var file in new DirectoryInfo(dir).GetFiles())
            {
                if (file.Name.EndsWith(".meta")) { continue; }
                file.Delete();
            }
        }

        private static void OpenExcel(string filePath)
        {
            if (Path.GetFileName(filePath).StartsWith("~$")) return;

            m_ExcelFilePath = filePath;
            using (FileStream fs = new FileStream(m_ExcelFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                IWorkbook workbook = new XSSFWorkbook(fs);

                if (workbook != null)
                {
                    int sheetNums = workbook.NumberOfSheets;
                    for (int i = 0; i < sheetNums; i++)
                    {
                        ISheet sheet = workbook.GetSheetAt(i);
                        WriteData(sheet);
                    }
                }
            }
        }

        // 两个文件：序列化后二进制文件、明文数据
        private static void WriteData(ISheet sheet)
        {
            List<string> repeatedList = new List<string>(); //已经处理过的数组
            int Nums = sheet.LastRowNum; // 行数
            if (Nums > 4)
            {
                object configIns = m_ConfigDllAssembly.CreateInstance("Deploy." + sheet.SheetName + "_Map"); // 创建数据容器类
                if (configIns == null)
                {
                    Console.WriteLine($">> 转换失败 >> 可能存在重复的Excel表Sheet名称！ExcelPath-{m_ExcelFilePath} SheetName-{sheet.SheetName}");
                    return;
                }

                Type configType = configIns.GetType();
                PropertyInfo propertyInfo = configType.GetProperty("Items"); // 取到容器字段
                object ItemsVal = propertyInfo.GetValue(configIns); // 取到容器

                IRow row = sheet.GetRow(0); // 第一行数据
                int cellNum = row.LastCellNum;

                List<int> validColIndex = new List<int>(); //有效列 #是注释列
                for (int i = 0; i < cellNum; i++)
                {
                    if (row.GetCell(i).StringCellValue == "#") continue;
                    if (row.GetCell(i).StringCellValue != "__END__")
                    {
                        validColIndex.Add(i);
                    }
                    else
                    {
                        break;
                    }
                }

                IRow nameRow = sheet.GetRow(1); // 字段行数据
                IRow typeRow = sheet.GetRow(3); // 类型行数据
                for (int i = 4; i < Nums; i++)
                {
                    IRow rowData = sheet.GetRow(i);
                    if (rowData == null) { continue; }                       // 空行跳过，避免空引用
                    ICell firstCell = rowData.GetCell(0);
                    if (firstCell != null && firstCell.ToString() == "__END__") { break; }  // 结束标志放在第一列

                    //检查并记录ID
                    int id = 0;
                    var idCell = rowData.GetCell(validColIndex[0]);
                    string idString = idCell != null ? idCell.ToString().Trim() : string.Empty;
                    if (string.IsNullOrEmpty(idString)) { continue; }
                    id = int.Parse(idString, Inv);

                    object dataIns = m_ConfigDllAssembly.CreateInstance("Deploy." + sheet.SheetName); // 数据实例
                    for (int j = 0; j < validColIndex.Count; j++)
                    {
                        int index = validColIndex[j];
                        var typeCell = typeRow.GetCell(index);
                        string type = typeCell != null ? typeCell.ToString().Trim() : string.Empty;
                        var fieldNameCell = nameRow.GetCell(index);
                        string fieldName = fieldNameCell != null ? fieldNameCell.ToString().Trim() : string.Empty;
                        var valueCell = rowData.GetCell(index);
                        string value = valueCell != null ? valueCell.ToString().Trim() : string.Empty;

                        //是否为repeated数据
                        if (fieldName.Contains("_"))
                        {
                            fieldName = fieldName.Split('_')[0];
                            if (repeatedList.Contains(fieldName))
                            {
                                continue;
                            }
                            else
                            {
                                repeatedList.Add(fieldName);
                                type = $"{type}Array";
                                value = $"[ {value}";
                                //遍历获取所有repeated数据
                                for (int k = 0; k < validColIndex.Count; k++)
                                {
                                    string fieldNameRpt = nameRow.GetCell(validColIndex[k]) != null ? nameRow.GetCell(validColIndex[k]).ToString() : string.Empty;
                                    if (fieldNameRpt.Contains("_") && fieldNameRpt.StartsWith(fieldName))
                                    {
                                        string valueRpt = rowData.GetCell(validColIndex[k]) != null ? rowData.GetCell(validColIndex[k]).ToString() : string.Empty;
                                        value = $"{value},{valueRpt}";
                                    }
                                }
                                value = $"{value} ]";
                            }
                        }

                        if (value.Length > 0)
                        {
                            //采用字段不使用属性是因为repeated没有set方法
                            string first = fieldName.First().ToString().ToLower();
                            string sub = fieldName.Substring(1);
                            FieldInfo fieldInfo = dataIns.GetType().GetField($"{first}{sub}_", BindingFlags.NonPublic | BindingFlags.Instance);
                            try
                            {
                                object realVal = GetRealVal(type, value); //获取真实值
                                fieldInfo.SetValue(dataIns, realVal);
                            }
                            catch (Exception)
                            {
                                Console.WriteLine($">> 转换失败 >> 值或类型不合法 filePath-{m_ExcelFilePath} value-{value} type-{type}");
                            }
                        }
                    }

                    //Add进Map
                    MethodInfo addMethod = propertyInfo.PropertyType.GetMethod("Add", new Type[] { typeof(int), dataIns.GetType() }); // 获取容器Add方法,需要标明具体添加哪种类型
                    try
                    {
                        addMethod?.Invoke(ItemsVal, new[] { id, dataIns });
                    }
                    catch (Exception)
                    {
                        Console.WriteLine($">> 转换失败 >> 重复的ID filePath-{m_ExcelFilePath} ID-{id}");
                    }
                }
                SaveCfgSerializeFile(configIns, sheet.SheetName);
                SaveCfgTxtFile(configIns, sheet.SheetName);
            }
        }

        // 根据类型获取真实值
        //  *********** 根据自己的填表规则进行修改 ***********
        private static object GetRealVal(string type, string value)
        {
            switch (type)
            {
                case "string":
                    return value;
                case "stringArray":
                case "stringarray":
                    return HandlerArray(value, (sVal) => { return sVal.Trim(); });
                case "int":
                    return int.Parse(value, Inv);
                case "intArray":
                case "intarray":
                    return HandlerArray(value, (sVal) => { return int.Parse(sVal, Inv); });
                case "float":
                    return float.Parse(value, Inv);
                case "floatArray":
                case "floatarray":
                    return HandlerArray(value, (sVal) => { return float.Parse(sVal, Inv); });
                case "map<int,int>":
                    return HandlerMap(value, (sVal1) => { return int.Parse(sVal1, Inv); }, (sVal2) => { return int.Parse(sVal2, Inv); });
                case "map<int,string>":
                    return HandlerMap(value, (sVal1) => { return int.Parse(sVal1, Inv); }, (sVal2) => { return sVal2.Trim(); });
                case "map<string,string>":
                    return HandlerMap(value, (sVal1) => { return sVal1.Trim(); }, (sVal2) => { return sVal2.Trim(); });
                case "map<string,int>":
                    return HandlerMap(value, (sVal1) => { return sVal1.Trim(); }, (sVal2) => { return int.Parse(sVal2, Inv); });
                default:
                    Console.WriteLine($">> 转换失败 >> 类型不合法 filePath-{m_ExcelFilePath} type-{type}");
                    return value;
            }
        }

        // 返回数组，[]包裹 ,分隔
        private static RepeatedField<T> HandlerArray<T>(string value, Func<string, T> func)
        {
            if (null == value) { return null; }

            string val = value.TrimStart('[');
            val = val.TrimEnd(']');
            string[] datas = val.Split(',');
            RepeatedField<T> valArray = new RepeatedField<T>();
            for (int i = 0; i < datas.Length; i++)
            {
                valArray.Add(func(datas[i]));
            }
            return valArray;
        }

        // 返回Map，{}包裹 ,分隔
        private static MapField<T1, T2> HandlerMap<T1, T2>(string value, Func<string, T1> func1, Func<string, T2> func2)
        {
            if (null == value) { return null; }

            string val = value.TrimStart('{');
            val = val.TrimEnd('}');
            string[] datas = val.Split(',');
            MapField<T1, T2> valMap = new MapField<T1, T2>();
            for (int i = 0; i < datas.Length; i++)
            {
                string[] sval = datas[i].Split(':');
                T1 k = func1(sval[0]);
                T2 v = func2(sval[1]);
                valMap.Add(k, v);
            }
            return valMap;
        }

        // 序列化后二进制文件
        private static void SaveCfgSerializeFile(object obj, string sheetName)
        {
            string fileName = $"{sheetName}.bytes";
            string filePath = Path.Combine(m_CfgBytesPathDir, fileName);
            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                MessageExtensions.WriteTo(obj as IMessage, fs);
                Console.WriteLine($">> 转换完成 >> {fileName}");
            }
        }

        // 明文数据
        private static void SaveCfgTxtFile(object obj, string sheetName)
        {
            string fileName = sheetName + ".txt";
            // 直接以 UTF-8（无 BOM）写出明文，避免旧代码中依赖系统默认编码的乱码转换
            File.WriteAllText(Path.Combine(m_CfgTxtPathDir, fileName), obj.ToString(), Utf8NoBom);
        }
    }
}
