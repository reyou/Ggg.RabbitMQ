using System;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using TestUtilities;

namespace RpcClientInterfaceConsole
{
    /// <summary>
    /// https://github.com/reyou/Ggg.RabbitMQ/wiki/Message-properties
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            TestUtilitiesClass.WriteLine("Program.Main Started.");
            RunClient();
            RunServer();
            TestUtilitiesClass.WriteLine("main finished.");
            Console.ReadLine();
        }

        private static void RunServer()
        {
            TaskFactory factory = new TaskFactory();
            factory.StartNew(RpcServer.Main2);
        }

        private static void RunClient()
        {
            TaskFactory factory = new TaskFactory();
            factory.StartNew(Rpc.Main2);
        }
    }

}
