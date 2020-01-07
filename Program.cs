using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp11
{
    class Program
    {
        static void Main(string[] args)
        {
            RemoteEbs rem = new RemoteEbs();
            rem.StartServer();
            Console.ReadKey();
        }
    }
}
