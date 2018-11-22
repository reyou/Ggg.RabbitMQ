using System;
using System.Text;

namespace RoundRobinDispatchingConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            MessageCreator.Start(10);
            MessageReader.Read();
            MessageReader2.Read();
            MessageReader3.Read();
            Console.WriteLine("Finish");
            Console.ReadLine();
        }
    }
}

