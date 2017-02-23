using ChatLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Practices.Unity;
using Ninject.Modules;
using Ninject;
using LoggerLibrary;
//using LogLib;

namespace ChatUi
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new ChatForm(new Client(new Logger())));//constructor injection

            //Unity IOC container

            //UnityContainer container = new UnityContainer();
            //container.RegisterType<ILoggingService, Logger>(); // text file logger
            //container.RegisterType<ILoggingService, MikeSturdy_logger>(); // log4net console logger
            //Application.Run(container.Resolve<ChatForm>());//unity constructor injection




            //ninject IOC
            //StandardKernel kernel = new StandardKernel();
            //kernel.Bind<ILoggingService>().To<Logger>();
            //Application.Run(kernel.Get<ChatForm>());

            //nicks logger using nlog

            UnityContainer container = new UnityContainer();
            container.RegisterType<ILoggingService, NickBourque_Logger>();
            Application.Run(container.Resolve<ChatForm>());
        }
    }
}
