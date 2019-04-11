using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JinhuasuanSpiderTool
{
    public class LogHelper
    {
        private static string baseLogDir = @"D:\jinhuasuan\Log";

        public static void WriteLogs(string message, string logFile = "记录文件")
        {
            try
            {
                if (Directory.Exists(baseLogDir))
                {
                    Directory.CreateDirectory(baseLogDir);
                }
                string filepath = "";
                if (logFile.Contains("\\")) filepath = logFile;
                else filepath = Path.Combine(baseLogDir, logFile + ".log");
                if (!Directory.Exists(baseLogDir))
                {
                    Directory.CreateDirectory(baseLogDir);
                }
                using (StreamWriter sw = new StreamWriter(filepath, true))
                {
                    sw.WriteLine(message);
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
