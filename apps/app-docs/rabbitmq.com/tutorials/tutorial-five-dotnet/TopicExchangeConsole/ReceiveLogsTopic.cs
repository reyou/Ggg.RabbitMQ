using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using TestUtilities;

namespace TopicExchangeConsole
{
    class ReceiveLogsTopic
    {
        public static void Main2(string[] args)
        {
            ConnectionFactory factory = TestUtilitiesClass.GetConnectionFactory();
            IConnection connection = factory.CreateConnection();
            IModel channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: "topic_logs", type: "topic");
            string queueName = channel.QueueDeclare().QueueName;

            foreach (string bindingKey in args)
            {
                channel.QueueBind(queue: queueName,
                    exchange: "topic_logs",
                    routingKey: bindingKey);
            }

            Console.WriteLine("[*] Waiting for messages. To exit press CTRL+C");

            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                byte[] body = ea.Body;
                RabbitMqMessage rabbitMqMessage = TestUtilitiesClass.ParseMessage(body);
                string routingKey = ea.RoutingKey;
                Console.WriteLine("[x] Received '{0}':'{1}'",
                    routingKey,
                    rabbitMqMessage.ToJsonString());
                TestUtilitiesClass.SleepProducer();
            };
            channel.BasicConsume(queue: queueName,
                autoAck: true,
                consumer: consumer);


        }
    }

}
