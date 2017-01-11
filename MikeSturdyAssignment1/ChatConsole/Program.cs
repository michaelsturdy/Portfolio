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
            string resData = null;// variable for received data
            string text = null;//input variable for sending
            bool connected; // variable to see if there is a connection
            Parent client;//variable to hold server or client object

            if (args.Length > 0 && args[0] == "-server")
            {
                client = new Server();
                Console.WriteLine("Waiting for a connection...");
                connected = client.Connect();
                if (connected)//if a connection is established open the stream
                {
                    Console.WriteLine("Connected");
                    client.Stream();
                }
                else
                {
                    Console.WriteLine("Unable to establish a connection");
                }



            }

            else
            {
               client = new Client();
               connected = client.Connect();
                if (connected)
                {
                    Console.WriteLine("Connected");
                    client.Stream();
                }
                else
                {
                    Console.WriteLine("Unable to connect to the server");
                    Console.Read();
                    Environment.Exit(0);
                }
                             
            }//end else
            Console.WriteLine("Press I to enter input mode and enter to send");
            Console.WriteLine("Type quit as a message to exit");
            while (true)//listening loop
                {
                   resData = client.Recieve();
              
                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo keyinfo = Console.ReadKey(true);
                        if (keyinfo.Key == ConsoleKey.I)
                        {
                            Console.Write(">>");
                            text = Console.ReadLine();
                            if (text == "quit")
                            {
                                Console.WriteLine("Good Bye");
                                Environment.Exit(0);
                            }
                        try
                        {
                            client.Send(text);//send the message
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine("Connection Lost");
                            Console.WriteLine(e);
                            Console.Read();
                            Environment.Exit(0);
                        }
                            
                        }

                        else
                        {
                            Console.WriteLine("Press I to send a message");
                        }
                    }
                    if (resData != null)
                    { 
                        Console.WriteLine("Received: {0}", resData);//display message once it has been received
                        resData = null;
                    }
                    
                }
            
        }
    }
}
