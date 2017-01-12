using System;
using System.Net.Sockets;


namespace ChatLib2
{
    public abstract class Parent
    {

        public TcpClient client;//Variable to hold the TcpClient object
        public NetworkStream stream;//Variable to hold the NetworkStream object
        Byte[] data = new Byte[256];//Byte array to send or receive from the stream
        /// <summary>
        /// Abstract connect method for client and server
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
        /// Listens for incoming data from the stream
        /// </summary>
        /// <returns>Data received as a string</returns>
        public string Recieve()
        {
            data = new Byte[256];
            String responseData = String.Empty;//Empty string to hold the response Data

           
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
