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

        public void LogMessage(string Message)
        {
            using (StreamWriter sw = new StreamWriter("C:/Users/NSCC Student/Desktop/Logs.txt"))
            {
                sw.WriteLine(Message);
            }
        }
    }
}
