using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TasksLib
{
    public class Executor
    {
        public bool StopExecution = false;
       public event ProgressChangedEventHandler ProgressChanged;


        public void DoSomething()
        {
            for (int i = 1; i <= 100; i++)
            {
                if (StopExecution) { ProgressChanged(0); break; };
                {
                    //do work
                    Thread.Sleep(100);
                    // broadcast work done
                    //raise an event
                    ProgressChanged(i);
                }
                
                

               
            }

        }
    }
}
