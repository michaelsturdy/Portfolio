using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatLib2
{
    public class Server : Parent
    {
        
        public override bool Connect()
        {

            Int32 port = 13000;
            IPAddress addr = IPAddress.Parse("127.0.0.1");
            TcpListener tcp = new TcpListener(addr, port);
            tcp.Start();

            while (true)
            {
                client = tcp.AcceptTcpClient();
                return true;
            }

        }//end connect
        

    }
}
