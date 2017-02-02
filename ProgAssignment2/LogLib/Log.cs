using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogLib
{
    public class Log
    {
        /// <summary>
        /// Writes to the log file
        /// </summary>
        /// <param name="Message">message to write to the log</param>
        public void LogMessage(string Message)
        {
            using (StreamWriter sw = new StreamWriter("C:/Users/NSCC Student/Desktop/Logs.txt",true))
            {
                sw.WriteLine(Message);
            }
        }
    }
}
