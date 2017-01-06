using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatLib;

namespace ChatLib
{
    class Program
    {
        static void Main(string[] args)
        {

            if (args.Length > 0 && args[0] == "-server")
            {
                Server server = new Server();
            }
            else
            {
                Client client = new Client();
            }

        }
    }
}
