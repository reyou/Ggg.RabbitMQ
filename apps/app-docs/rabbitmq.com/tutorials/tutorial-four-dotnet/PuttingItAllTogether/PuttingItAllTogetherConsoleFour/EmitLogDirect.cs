using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RabbitMQ.Client;
using TestUtilities;

namespace PuttingItAllTogetherConsoleFour
{
    class EmitLogDirect
    {
        public static void Main2(string severity)
        {
            TestUtilitiesClass.PrintThreadId("EmitLogDirect");
            ConnectionFactory factory = TestUtilitiesClass.GetConnectionFactory();
            using (IConnection connection = factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "direct_logs", type: ExchangeType.Direct);

                byte[] body = TestUtilitiesClass.GetMessage("EmitLogDirect " + severity);
                RabbitMqMessage rabbitMqMessage = TestUtilitiesClass.ParseMessage(body);
                channel.BasicPublish(exchange: "direct_logs",
                    routingKey: severity,
                    basicProperties: null,
                    body: body);
                Console.WriteLine("[EmitLogDirect] Sent '{0}':'{1}'", severity, rabbitMqMessage);
                TestUtilitiesClass.SleepProducer();
            }

        }
    }
}
