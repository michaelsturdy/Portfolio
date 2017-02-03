using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatLib
{
    /// <summary>
    /// handles messages from another thread
    /// </summary>
    public class MessageReceivedEventArgs : EventArgs
    {
        /// <summary>
        /// Received message
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Sets the Message Property
        /// </summary>
        /// <param name="message">Message to set to the property</param>
        public MessageReceivedEventArgs(string message)
        {
            Message = message;
        }
    }
}
