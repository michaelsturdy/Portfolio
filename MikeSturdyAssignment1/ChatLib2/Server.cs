using System;
using System.Net;
using System.Net.Sockets;


namespace ChatLib2
{
    public class Server : Parent
    {
        /// <summary>
        /// Starts listening for a connection from a client 
        /// </summary>
        /// <returns>True when a connection is made</returns>
        public override bool Connect()
        {

            Int32 port = 13000;
            IPAddress address = IPAddress.Parse("127.0.0.1");
            TcpListener listener = new TcpListener(address, port);
            listener.Start();

            while (true)
            {
                Client = listener.AcceptTcpClient();
                return true;
            }

        }
        

    }
}
