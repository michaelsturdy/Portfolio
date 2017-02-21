using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogLib
{
    /// <summary>
    /// log4net logger
    /// </summary>
    public class MikeSturdy_logger : ILoggingService
    {
        ILog logger;
        /// <summary>
        /// Constructor
        /// </summary>
        public MikeSturdy_logger()
        {
            logger = LogManager.GetLogger(typeof(MikeSturdy_logger));
            BasicConfigurator.Configure();
        }
        /// <summary>
        /// Logs a message
        /// </summary>
        /// <param name="message">Message to be logged</param>
        public void Log(string message)
        {
            logger.Debug(message);
        }
    }
}
