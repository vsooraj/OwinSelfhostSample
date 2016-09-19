using System;
using System.IO;

namespace miGuard
{
    public static class Logger
    {
        public static void WriteLog(string message)
        {

            using (StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt", true))
            {
                sw.WriteLine(DateTime.Now.ToString() + ":" + message.Trim());
                sw.Flush();
            }

        }
        public static void WriteException(Exception exception)
        {
            using (StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt", true))
            {
                sw.WriteLine(DateTime.Now.ToString() + ":" + exception.Source.ToString().Trim() + ":" + exception.Message.ToString());
                sw.Flush();
            }
        }
    }
}
