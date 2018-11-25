using System;
using System.Threading.Tasks;

namespace PuttingItAllTogetherConsoleFour
{
    class Program
    {
        static void Main(string[] args)
        {
            RunProducer();
            RunConsumer();
            Console.WriteLine("main final.");
            Console.ReadLine();
        }

        private static void RunConsumer()
        {
            TaskFactory factory = new TaskFactory();
            factory.StartNew(() =>
            {
                string[] severities = new[] { "info", "warning", "error" };
                ReceiveLogsDirect.Main2(severities);
            });
        }

        private static void RunProducer()
        {
            TaskFactory factory = new TaskFactory();
            factory.StartNew(() =>
            {
                for (int i = 0; i < 40; i++)
                {
                    string severity = GetSeverity(i);
                    EmitLogDirect.Main2(severity);
                }
            });
        }

        private static string GetSeverity(int i)
        {
            if (i % 2 == 0)
            {
                return "info";
            }
            if (i % 3 == 0)
            {
                return "warning";
            }
            if (i % 5 == 0)
            {
                return "error";
            }
            return "debug";
        }
    }
}
