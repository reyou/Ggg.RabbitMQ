using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using TestUtilities;

namespace PuttingItAllTogetherConsole
{
    class ReceiveLogs
    {
        public static void Main2()
        {
            TestUtilitiesClass.PrintThreadId("ReceiveLogs");
            ConnectionFactory factory = TestUtilitiesClass.GetConnectionFactory();
            IConnection connection = factory.CreateConnection();
            IModel channel = connection.CreateModel();
            channel.ExchangeDeclare(exchange: "logs", type: "fanout");
            string queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queue: queueName,
                exchange: "logs",
                routingKey: "");
            Console.WriteLine(" [*] Waiting for logs.");
            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                byte[] body = ea.Body;
                RabbitMqMessage rabbitMqMessage = TestUtilitiesClass.ParseMessage(body);
                Console.WriteLine("[ReceiveLogs]: {0}", rabbitMqMessage.ToJsonString());
                TestUtilitiesClass.SleepConsumer();
            };
            channel.BasicConsume(queue: queueName,
                autoAck: true,
                consumer: consumer);
        }
    }
}