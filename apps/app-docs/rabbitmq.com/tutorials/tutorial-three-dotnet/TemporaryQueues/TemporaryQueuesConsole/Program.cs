using System;
using System.Diagnostics;
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
            IConnection connection = connectionFactory.CreateConnection();
            IModel channel = connection.CreateModel();
            string queueName = channel.QueueDeclare().QueueName;
            string queueName2 = channel.QueueDeclare().QueueName;
            string queueName3 = channel.QueueDeclare().QueueName;
            Console.WriteLine("Temp Queue Created: " + queueName);
            channel.ExchangeDeclare("tempExc", ExchangeType.Direct);
            channel.QueueBind(queueName, "tempExc", "routingKey1");
            channel.QueueBind(queueName2, "tempExc", "routingKey2");
            channel.QueueBind(queueName3, "tempExc", "routingKey1");
            for (int i = 0; i < counter; i++)
            {
                byte[] message = TestUtilitiesClass.GetMessage("TempQueueCreator");
                RabbitMqMessage rabbitMqMessage = TestUtilitiesClass.ParseMessage(message);
                channel.BasicPublish(exchange: "tempExc", routingKey: "routingKey1", basicProperties: null, body: message);
                Console.WriteLine("Sent {0}", rabbitMqMessage.ToJsonString());
            }
        }
    }
}
