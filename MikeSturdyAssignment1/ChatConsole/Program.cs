using System;
using ChatLib2;



//https://msdn.microsoft.com/en-us/library/system.net.sockets.tcplistener(v=vs.110).aspx


namespace ChatConsole
{
    class Program
    {
        
        static void Main(string[] args)
        {
            string responseData = null;// Variable for received data
            string text = null;//Input variable for sending
            bool connected; // Variable to see if there is a connection
            Parent client;//Variable to hold server or client object

            if (args.Length > 0 && args[0] == "-server")
            {
                client = new Server();
                Console.WriteLine("Waiting for a connection...");
                connected = client.Connect();
                if (connected)
                {
                    Console.WriteLine("Connected");
                    client.OpenStream();
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
                    client.OpenStream();
                }
                else
                {
                    Console.WriteLine("Unable to connect to the server");
                    Console.Read();
                    Environment.Exit(0);
                }
                             
            }
            Console.WriteLine("Press I to enter input mode and enter to send");
            Console.WriteLine("Type quit as a message to exit");
            while (true)//Listening loop
                {
                   responseData = client.Recieve();
              
                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo keyinfo = Console.ReadKey(true);
                        if (keyinfo.Key == ConsoleKey.I)
                        {
                            Console.Write(">>");
                            text = Console.ReadLine();
                            if (text == "quit")
                            {
                            text = "The other person has left the session";
                                try
                                {
                                    client.Send(text);//Informs the other user you have disconnected
                                }
                                catch (Exception e)
                                {
                                Environment.Exit(0);
                                }
                                Console.WriteLine("Good Bye");
                                Environment.Exit(0);
                            }
                        try
                        {
                            client.Send(text);//Send the message
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine("Connection Lost");
                            Console.Read();
                            Environment.Exit(0);
                        }
                            
                        }

                        else
                        {
                            Console.WriteLine("Press I to send a message");
                        }
                    }
                    if (responseData != null)
                    { 
                        Console.WriteLine("Received: {0}", responseData);//Display the message once it has been received
                        responseData = null;
                    }
                    
                }
            
        }
    }
}
