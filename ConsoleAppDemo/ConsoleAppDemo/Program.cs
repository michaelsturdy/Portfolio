using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleAppDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World");
            // Console.Write("Enter your name: ");
            //string input = Console.ReadLine();
            //Console.WriteLine("hello " + input);
            //Console.WriteLine(String.Format("hello {0} ! :)",input));
            //Console.Read();

            Console.WriteLine("Hit the any key ");
            

            while(true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyinfo = Console.ReadKey(true);
                    if (keyinfo.Key == ConsoleKey.I)
                    {
                        Console.WriteLine("You pressed 'I'");
                    }

                    else
                    {
                        Console.WriteLine("you didnt press i ");
                    }
                }
                Console.WriteLine("looping");
                Thread.Sleep(200);
            }

            

            Console.Read();
        }
    }
}
