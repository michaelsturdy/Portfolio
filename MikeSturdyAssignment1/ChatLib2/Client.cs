using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatLib2
{
    public class Client
    {
        TcpClient client;
        NetworkStream stream;
        Byte[] data = new Byte[256];

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
        public void Stream()
        {
            stream = client.GetStream();
        }

        public void Send(string msg)
        {
            data = System.Text.Encoding.ASCII.GetBytes(msg);

            stream.Write(data, 0, data.Length);
        }

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

    }
}
