using System;
using System.Threading;

namespace FairDispatchConsole
{
    /// <summary>
    /// https://www.rabbitmq.com/tutorials/tutorial-two-dotnet.html
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            NewTask.Main2(10);
            Worker.Main2();
            Console.WriteLine("FairDispatchConsole Finish. ThreadId: " + Thread.CurrentThread.ManagedThreadId);
            Console.ReadLine();
        }
    }
}
