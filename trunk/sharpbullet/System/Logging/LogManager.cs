using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;

namespace System.Logging
{
    public class LogManager
    {
        public string Directory { get; set; }

        public LogManager()
        {
        }

        public string Log(LogType type, string message)
        {
            if (string.IsNullOrEmpty(Directory))
                throw new Exception("LogManager: Directory is not set!");

            string filename = Directory;
            if (!filename.EndsWith("/")) filename += "/";
            filename += DateTime.Today.ToString("yyyy-mm-dd") + ".txt";
            if (type == LogType.ERROR)
                filename = "E " + filename;

            message = DateTime.Now.ToString("yyyy-mm-dd HH:mm:ss")+ "\t" + type+ "\t" + message + Environment.NewLine;

            if (type == LogType.IGNORE) return message;

            File.AppendAllText(filename, message);

            return message;
        }
    }
}
