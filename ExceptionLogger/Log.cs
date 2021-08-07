using System;
using System.IO;
using System.Text;


namespace ExceptionLogger
{
    public sealed class Log : ILog
    {
        private static readonly Lazy<Log> instance = new Lazy<Log>(() => new Log());
        public static Log GetInstance
        {
            get { return instance.Value; }
        }

        private Log() { }

        public void LogException(string message)
        {
            var fileName = $"{"Exception"}_{DateTime.Now.ToShortDateString().Replace("/","-")}.log";
            var logFilePath = $@"{AppDomain.CurrentDomain.BaseDirectory}\{fileName}";
            var sb = new StringBuilder();
            sb.AppendLine("----------------------------------------");
            sb.AppendLine(DateTime.Now.ToString());
            sb.AppendLine(message);
            using (var writer = new StreamWriter(logFilePath, true))
            {
                writer.Write(sb.ToString());
                writer.Flush();
            }
        }
    }
}