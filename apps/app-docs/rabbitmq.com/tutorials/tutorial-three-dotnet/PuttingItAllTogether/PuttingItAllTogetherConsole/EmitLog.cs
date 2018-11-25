using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using RabbitMQ.Client;
using TestUtilities;

namespace PuttingItAllTogetherConsole
{
    class EmitLog
    {
        public static void Main2(int count)
        {
            TestUtilitiesClass.PrintThreadId("EmitLog");
            ConnectionFactory factory = TestUtilitiesClass.GetConnectionFactory();
            using (IConnection connection = factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);
                byte[] body = TestUtilitiesClass.GetMessage("PuttingItAllTogetherConsole");
                RabbitMqMessage rabbitMqMessage = TestUtilitiesClass.ParseMessage(body);
                for (int i = 0; i < count; i++)
                {
                    channel.BasicPublish(exchange: "logs",
                        routingKey: "",
                        basicProperties: null,
                        body: body);
                    Console.WriteLine("Sent {0}", rabbitMqMessage.ToJsonString());
                    TestUtilitiesClass.SleepProducer();
                }

            }
        }
    }
}

