using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthernetIP_Fanuc
{
    class Program
    {
        static void Main(string[] args)
        {
            Host host = new Host();
            host.Execute();
            Console.WriteLine("Press any key to contunue...");
            Console.ReadKey();
            host = null;
        }
    }
}
