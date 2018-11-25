using System;
using System.Threading.Tasks;

namespace PuttingItAllTogetherConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            TaskFactory factory = new TaskFactory();
            factory.StartNew(() => { EmitLog.Main2(20); });
            factory.StartNew(() => { ReceiveLogs.Main2(); });
            Console.ReadLine();
        }
    }
}
