using System;
using System.Net.Sockets;


namespace ChatLib2
{
    public abstract class Parent
    {

        public TcpClient Client;//Property to hold the TcpClient object
        public NetworkStream Stream;//Property to hold the NetworkStream object
        Byte[] Data = new Byte[256];//Byte array property to send or receive data from the stream
        /// <summary>
        /// Abstract connect method for client and server
        /// </summary>
        /// <returns>true if connection established</returns>
        public abstract bool Connect();
       /// <summary>
       /// Opens the stream
       /// </summary>
        public void OpenStream()
        {
            Stream = Client.GetStream();
        }
        /// <summary>
        /// Sends data through the stream
        /// </summary>
        /// <param name="message">Message to be sent</param>
        public void Send(string message)
        {
            Data = System.Text.Encoding.ASCII.GetBytes(message);

            Stream.Write(Data, 0, Data.Length);
        }
        /// <summary>
        /// Listens for incoming data from the stream
        /// </summary>
        /// <returns>Data received as a string</returns>
        public string Recieve()
        {
            Data = new Byte[256];
            String responseData = String.Empty;//Empty string to hold the response Data

           
            if (Stream.DataAvailable)
            {
                Int32 bytes = Stream.Read(Data, 0, Data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(Data, 0, bytes);

                return responseData;
            }
            return null;

        }
    }//end class
}
