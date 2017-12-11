using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClienteC1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Iniciando...");
            Conn Sock = new Conn();
            Sock.Config_Server();
            Console.WriteLine("Press Enter from initialize server.");
            Console.ReadLine();
            Console.WriteLine("Init...");
            Sock.Connect();
            Console.WriteLine("Press Enter from Disconnect.");
            Console.ReadLine();
            Sock.Disconnect();
            Console.WriteLine("Close read.");
            Console.WriteLine("Press Enter from exit.");
            Console.ReadLine();

        }
    }
}
