using System;
using RabbitMQ.Client;
using TestUtilities;

namespace ExchangeTypesConsole
{
    internal class FanoutLogsProducer
    {
        public static void Main2(int counter)
        {
            TestUtilitiesClass.PrintThreadId("FanoutLogsProducer.Main2");
            ConnectionFactory factory = TestUtilitiesClass.GetConnectionFactory();
            using (IConnection connection = factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {

                channel.ExchangeDeclare("logs", ExchangeType.Fanout);
                channel.QueueDeclare("logA",
                    durable: false,
                    exclusive: false,
                    autoDelete: false);
                channel.QueueDeclare("logB",
                    durable: false,
                    exclusive: false,
                    autoDelete: false);
                channel.QueueDeclare("logC",
                    durable: false,
                    exclusive: false,
                    autoDelete: false);
                channel.QueueBind("logA", "logs", "");
                channel.QueueBind("logB", "logs", "");
                channel.QueueBind("logC", "logs", "");
                byte[] message = TestUtilitiesClass.GetMessage("FanoutLogsProducer");
                RabbitMqMessage rabbitMqMessage = TestUtilitiesClass.ParseMessage(message);

                for (int i = 0; i < counter; i++)
                {
                    channel.BasicPublish(exchange: "logs",
                        routingKey: "",
                        basicProperties: null,
                        body: message);
                    Console.WriteLine("Published: " + rabbitMqMessage.ToJsonString());
                    TestUtilitiesClass.SleepProducer();
                }

            }
        }
    }
}