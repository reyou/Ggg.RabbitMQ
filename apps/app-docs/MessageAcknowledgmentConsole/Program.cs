using System;

namespace MessageAcknowledgmentConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            MessageCreator.Start(100);
            MessageReader.Read();
            Console.WriteLine("Finish");
            Console.ReadLine();
        }
    }
}
