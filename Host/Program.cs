using System;
using System.ServiceModel;

namespace Host
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(ServerChat.ServiceChat)))
            {
                host.Open();
                Console.WriteLine("Host started!");
                Console.ReadLine();
            }
        }
    }
}
