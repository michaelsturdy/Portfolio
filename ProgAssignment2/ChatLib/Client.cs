using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using LogLib;

namespace ChatLib
{
    /// <summary>
    /// Chat client
    /// </summary>
    public class Client
    {
        
        public event MessageReceivedEventHandler MessageReceived;   // event handler for received messages
        bool isListening;                                           //Property to check if the listening loop is on
        TcpClient client;                                           //Property to hold the TcpClient object
        NetworkStream Stream;                                       //Property to hold the NetworkStream object
        Byte[] Data = new Byte[256];                                //Byte array property to send or receive data from the stream
        Log log = new Log();                                        //logging object used to write to the log



        /// <summary>
        /// Connects a client to a server
        /// </summary>
        /// <returns>True if a connect was established</returns>
        public bool Connect()
        {
            Int32 port = 13000;
            string ip = "127.0.0.1";
            try
            {
                client = new TcpClient(ip, port);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        /// <summary>
        /// Closes the tcpClient connection to the listener
        /// </summary>
        /// <returns>False if connection closed</returns>
        public bool Close()
        {
            try
            {
                isListening = false;
                client.Close();
                return false;
            }
            catch
            {
                return true; 
            }
        }
        /// <summary>
        /// opens the stream
        /// </summary>
        public void OpenStream()
        {
            Stream = client.GetStream();
        }


        /// <summary>
        /// Sends data through the stream and logs it
        /// </summary>
        /// <param name="message">Message to be sent</param>
        public void Send(string message)
        {
            log.LogMessage(DateTime.Now + " Client: " + message);
            Data = System.Text.Encoding.ASCII.GetBytes(message);
                Stream.Write(Data, 0, Data.Length);
        }


        /// <summary>
        /// Listens for incoming data from the stream. sends it to a messageRecived event and logs it
        /// </summary
        public void Receive()
        {
            isListening = true;
            while (isListening)
            {
                Data = new Byte[256];
                String responseData = String.Empty;//Empty string to hold the response Data


                if (Stream.DataAvailable)
                {
                    Int32 bytes = Stream.Read(Data, 0, Data.Length);
                    responseData = System.Text.Encoding.ASCII.GetString(Data, 0, bytes);
                    log.LogMessage(DateTime.Now + " Server: " + responseData);
                    MessageReceived(this, new MessageReceivedEventArgs(responseData));

                }

            }
        }


    }//end class
}
