using System;
using System.Net.Sockets;


namespace ChatLib2
{
    public abstract class Parent
    {

        public TcpClient client;
        public NetworkStream stream;
        Byte[] data = new Byte[256];
        /// <summary>
        /// abstract connect method for client and server
        /// </summary>
        /// <returns>true if connection established</returns>
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
        /// <param name="message">Message to be sent</param>
        public void Send(string message)
        {
            data = System.Text.Encoding.ASCII.GetBytes(message);

            stream.Write(data, 0, data.Length);
        }
        /// <summary>
        /// listens for incoming data 
        /// </summary>
        /// <returns>data received as string</returns>
        public string Recieve()
        {
            data = new Byte[256];
            String responseData = String.Empty;//empty string to hold the response

           
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
