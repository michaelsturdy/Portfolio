using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatLib2;
using System.Threading;


//https://msdn.microsoft.com/en-us/library/system.net.sockets.tcplistener(v=vs.110).aspx

namespace ChatConsole
{
    class Program
    {
        
        static void Main(string[] args)
        {
            string resData = null;
            string text = null;

            if (args.Length > 0 && args[0] == "-server")
            {
                Server server = new Server();
                Console.WriteLine("Waiting for a connection...");
                server.Connect();
                Console.WriteLine("Connected!");
                server.Stream();

                while (true)
                {
                    resData = server.Recieve();
                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo keyinfo = Console.ReadKey(true);
                        if (keyinfo.Key == ConsoleKey.I)
                        {
                            Console.WriteLine(">>");
                            text = Console.ReadLine();
                            if (text == "quit")
                            {
                                Console.WriteLine("Good Bye");
                                Environment.Exit(0);
                            }
                            server.Send(text);
                        }

                        else
                        {
                            Console.WriteLine("you didnt press i ");
                        }
                    }
                    if (resData != null)
                    {
                        Console.WriteLine("Received: {0}", resData);
                        resData = null;
                    }

                }

                Console.Read();
            }

            else
            {
                Client client = new Client();
               bool connected = client.Connect();
                if (connected)
                {
                    client.Stream();
                }
                else
                {
                    Console.WriteLine("unable to connect to the server");
                }

                while (true)
                {
                    resData = client.Recieve();
                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo keyinfo = Console.ReadKey(true);
                        if (keyinfo.Key == ConsoleKey.I)
                        {
                            Console.WriteLine(">>");
                            text = Console.ReadLine();
                            if (text == "quit")
                            {
                                Console.WriteLine("Good Bye");
                                Environment.Exit(0);
                            }
                            client.Send(text);
                        }

                        else
                        {
                            Console.WriteLine("you didnt press i ");
                        }
                    }
                    if (resData != null)
                    { 
                        Console.WriteLine("Received: {0}", resData);
                        resData = null;
                    }
                    
                }
                             
            }


            Console.Read();
        }
    }
}
