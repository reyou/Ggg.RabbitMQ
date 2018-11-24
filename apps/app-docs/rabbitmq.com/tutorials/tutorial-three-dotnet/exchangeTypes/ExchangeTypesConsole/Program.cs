using System;
using System.Text;
using TestUtilities;

namespace ExchangeTypesConsole
{
    /// <summary>
    /// https://dotnetcodr.com/2016/08/15/messaging-with-rabbitmq-and-net-review-part-6-the-fanout-exchange-type/
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            FanoutLogsProducer.Main2(10);
            TestUtilitiesClass.PrintThreadId("Program.Main");
            Console.ReadLine();
        }
    }
}
