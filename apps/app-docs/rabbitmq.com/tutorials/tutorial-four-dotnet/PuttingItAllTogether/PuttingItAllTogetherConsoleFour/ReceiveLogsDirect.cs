using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using TestUtilities;

namespace PuttingItAllTogetherConsoleFour
{
    class ReceiveLogsDirect
    {
        public static void Main2(string[] args)
        {
            ConnectionFactory factory = TestUtilitiesClass.GetConnectionFactory();
            IConnection connection = factory.CreateConnection();
            IModel channel = connection.CreateModel();
            channel.ExchangeDeclare(exchange: "direct_logs",
                type: "direct");
            string queueName = channel.QueueDeclare().QueueName;

            foreach (string severity in args)
            {
                channel.QueueBind(queue: queueName,
                    exchange: "direct_logs",
                    routingKey: severity);
            }

            Console.WriteLine(" [*] Waiting for messages.");

            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                byte[] body = ea.Body;
                RabbitMqMessage rabbitMqMessage = TestUtilitiesClass.ParseMessage(body);
                string routingKey = ea.RoutingKey;
                Console.WriteLine(" [x] Received '{0}':'{1}'",
                    routingKey, rabbitMqMessage.ToJsonString());
                TestUtilitiesClass.SleepConsumer();
            };
            channel.BasicConsume(queue: queueName,
                autoAck: true,
                consumer: consumer);
        }
    }

}
