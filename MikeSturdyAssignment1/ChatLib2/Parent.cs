using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatLib2
{
    public abstract class Parent
    {

        public TcpClient client = null;
        public NetworkStream stream = null;
        Byte[] data = new Byte[256];

        public abstract bool Connect();
       /// <summary>
       /// Opens the stream
       /// </summary>
        public void Stream()
        {
            stream = client.GetStream();
        }
        /// <summary>
        /// Sends data through the stream
        /// </summary>
        /// <param name="msg">Message to be sent</param>
        public void Send(string msg)
        {
            data = System.Text.Encoding.ASCII.GetBytes(msg);

            stream.Write(data, 0, data.Length);
        }
        /// <summary>
        /// listens for incoming data 
        /// </summary>
        /// <returns>data received as string</returns>
        public string Recieve()
        {
            data = new Byte[256];

            // String to store the response ASCII representation.
            String responseData = String.Empty;

            // Read the first batch of the TcpServer response bytes.
            if (stream.DataAvailable)
            {
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);

                return responseData;
            }
            return null;

        }



    }//end class
}
