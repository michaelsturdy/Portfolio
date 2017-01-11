using System;
using System.Net.Sockets;


namespace ChatLib2
{
    public class Client : Parent
    {


        /// <summary>
        /// Connects a client to a server
        /// </summary>
        /// <returns></returns>
        public override bool Connect()
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
        

    }
}
