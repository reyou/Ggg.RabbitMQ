using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RabbitMQ.Client;
using TestUtilities;

namespace TopicExchangeConsole
{
    class EmitLogTopic
    {
        public static void Main2(string[] args, int count)
        {
            ConnectionFactory factory = TestUtilitiesClass.GetConnectionFactory();
            using (IConnection connection = factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "topic_logs", type: ExchangeType.Topic);
                string routingKey = (args.Length > 0) ? args[0] : "anonymous.info";
                byte[] message = (args.Length > 1)
                    ? TestUtilitiesClass.GetMessage(string.Join(" ", args.Skip(1).ToArray()))
                    : TestUtilitiesClass.GetMessage("Hello World!");
                for (int i = 0; i < count; i++)
                {
                    channel.BasicPublish(exchange: "topic_logs",
                        routingKey: routingKey,
                        basicProperties: null,
                        body: message);
                    RabbitMqMessage rabbitMqMessage = TestUtilitiesClass.ParseMessage(message);
                    Console.WriteLine("[x] Sent '{0}':'{1}'", routingKey, rabbitMqMessage.ToJsonString());
                    TestUtilitiesClass.SleepProducer();
                }

            }
        }

    }
}
