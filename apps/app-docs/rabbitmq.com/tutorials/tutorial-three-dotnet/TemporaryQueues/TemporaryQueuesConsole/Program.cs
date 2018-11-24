using System;
using RabbitMQ.Client;
using TestUtilities;

namespace TemporaryQueuesConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            TempQueueCreator.Create(10);
            TestUtilitiesClass.PrintThreadId("Program.Main");
            Console.ReadLine();
        }
    }

    internal class TempQueueCreator
    {
        public static void Create(int counter)
        {
            TestUtilitiesClass.PrintThreadId("TempQueueCreator.Create");
            ConnectionFactory connectionFactory = TestUtilitiesClass.GetConnectionFactory();
            using (IConnection connection = connectionFactory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    string queueName = channel.QueueDeclare().QueueName;
                    Console.WriteLine("Queue Created: " + queueName);
                    channel.ExchangeDeclare("tempExc", ExchangeType.Direct);
                    for (int i = 0; i < counter; i++)
                    {
                        byte[] message = TestUtilitiesClass.GetMessage("TempQueueCreator");
                        channel.BasicPublish(exchange: "tempExc", routingKey: queueName, basicProperties: null, body: message);
                    }
                }
            }
        }
    }
}
