﻿using System;
using System.Diagnostics;
using System.IO;

namespace ExcelToProtobuf
{
    // .proto文件转class
    public class GeneratClass
    {
        public static bool CallProtoc(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine($">> 转换失败 >> 无效的BuildProtos.bat文件路径-{path}");
                return false;
            }
            string dir = Path.GetDirectoryName(path);
            string fileName = Path.GetFileName(path);
            try
            {
                Process proc = new Process();
                proc.StartInfo.WorkingDirectory = dir;
                proc.StartInfo.FileName = fileName;
                proc.Start();
                proc.WaitForExit();
                return true;

            }
            catch (Exception e)
            {
                Console.WriteLine($">> 转换失败 >> BuildProtos.bat调用异常：{e.ToString()}");
                return false;
            }
        }
    }
}
