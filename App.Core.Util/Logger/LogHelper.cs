using System;
using System.IO;
using System.Text;

namespace App.Core.Util.Logger
{
    public class LogHelper
    {
        public static void WriteLog(Exception exception, string path)
        {
            try
            {
                var finalPath = path;
                var contents = new StringBuilder();
                var year = DateTime.Now.Year.ToString();
                var month = DateTime.Now.Month.ToString();
                var day = DateTime.Now.Day.ToString();
                finalPath = $"{finalPath}\\{year}";
                if (!Directory.Exists(finalPath))
                    Directory.CreateDirectory(finalPath);
                finalPath = $"{finalPath}\\{month}";
                if (!Directory.Exists(finalPath))
                    Directory.CreateDirectory(finalPath);
                finalPath = $"{finalPath}\\{day}.log";
                contents.Append($"{DateTime.Now:dd-MM-yyyy HH:mm:ss:fff} : {exception.Message} : {exception.StackTrace}" + Environment.NewLine);
                File.AppendAllText(finalPath, contents.ToString());
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
