using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatLib2
{
    public class Client : Parent
    {



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
