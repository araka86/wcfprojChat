using System;
using System.ServiceModel;

namespace ChatHost
{
    internal class Program
    {
        static void Main(string[] args)
        {

            using (var host = new ServiceHost(typeof(Wcf_Chat_Server.ServiceChat)))
            {
                host.Open();
                Console.WriteLine("Новый Хост Стартовал!!!");
                Console.ReadLine();
            }
        }
    }
}
