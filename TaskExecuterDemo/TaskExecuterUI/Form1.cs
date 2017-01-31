using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TasksLib;

namespace TaskExecuterUI
{
    public partial class TasksForm : Form
    {
        Thread workerThread;
        Executor executor = new Executor();
        public TasksForm()
        {
            executor.ProgressChanged += new TasksLib.ProgressChangedEventHandler(Executor_ProgressChanged);
            InitializeComponent();
        }

        private void Executor_ProgressChanged(int progress)
        {
            if (TasksProgressBar.InvokeRequired)
            {
                //lets threads talk to each other
                MethodInvoker invoker = new MethodInvoker(delegate() {
                    TasksProgressBar.Value = progress;
                    if (TasksProgressBar.Value == 100)
                    {
                        StartButton.Enabled = true;
                    }
                });
                TasksProgressBar.BeginInvoke(invoker);

            }
            else
            {
                //update progress bar
                TasksProgressBar.Value = progress;
                if (TasksProgressBar.Value == 100)
                {
                    StartButton.Enabled = true;
                }
            }
            
            
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            executor.StopExecution = false;
            StartButton.Enabled = false;
            cancelButton.Enabled = true;
            workerThread = new Thread(executor.DoSomething);
            workerThread.Name = "MyWorkerThread";
            workerThread.Start();

            
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            //cancel the worker thread
            //workerThread.Abort(); don't use!!
            executor.StopExecution = true;
            StartButton.Enabled = true;
            cancelButton.Enabled = false;
            
        }

        private void TasksForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (workerThread != null && workerThread.IsAlive)
            {
                executor.StopExecution = true;
                workerThread.Join();
            }
        }
    }
}
