using System;
using System.Threading.Tasks;

namespace TopicExchangeConsole
{
    /// <summary>
    /// https://www.rabbitmq.com/tutorials/tutorial-five-dotnet.html
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            string[] args2 = { "qqq", "www", "eee" };
            RunProducer(args2);
            RunConsumer(args2);
            Console.WriteLine("main finish.");
            Console.ReadLine();
        }

        private static void RunConsumer(string[] args2)
        {
            TaskFactory factory = new TaskFactory();
            factory.StartNew(() => { ReceiveLogsTopic.Main2(args2); });

        }

        private static void RunProducer(string[] args2)
        {
            TaskFactory factory = new TaskFactory();
            factory.StartNew(() => { EmitLogTopic.Main2(args2, 20); });
        }
    }
}
