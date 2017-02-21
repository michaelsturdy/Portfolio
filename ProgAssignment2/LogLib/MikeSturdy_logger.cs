using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogLib
{
    public class MikeSturdy_logger : ILoggingService
    {
        ILog logger;
        public MikeSturdy_logger()
        {
            logger = LogManager.GetLogger(typeof(MikeSturdy_logger));
            BasicConfigurator.Configure();
        }
        public void Log(string message)
        {
            logger.Debug(message);
        }
    }
}
