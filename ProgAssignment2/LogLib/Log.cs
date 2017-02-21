using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogLib
{
    /// <summary>
    /// Used for writing to a log file.
    /// </summary>
    public class Logger : ILoggingService
    {
        /// <summary>
        /// Writes to the log file
        /// </summary>
        /// <param name="Message">message to write to the log</param>
        public void Log(string Message)
        {
            string path;
            if (ConfigurationManager.AppSettings["LogPath"].Equals(""))
            {
                path = "logs.txt";
            }
            else
            {
                path = ConfigurationManager.AppSettings["LogPath"];
            }
            using (StreamWriter sw = new StreamWriter(path,true))
            {
                sw.WriteLine(Message);
            }
        }
    }
}
