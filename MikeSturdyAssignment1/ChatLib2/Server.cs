using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatLib2
{
    public class Server
    {
        TcpListener tcp = null;
        TcpClient client = null;
        NetworkStream stream = null;
        Byte[] data = null;
        
        public void Connect()
        {

            Int32 port = 13000;
            IPAddress addr = IPAddress.Parse("127.0.0.1");
            tcp = new TcpListener(addr, port);
            tcp.Start();

            while (true)
            {
                client = tcp.AcceptTcpClient();
                return;
            }

        }//end connect
        public void Stream()
        {
            stream = client.GetStream();
        }

        public void Send(string text)
        {
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(text);

            // Send back a response.
            stream.Write(msg, 0, msg.Length);
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
