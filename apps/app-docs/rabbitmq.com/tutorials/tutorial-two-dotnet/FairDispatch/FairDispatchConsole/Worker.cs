using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using TestUtilities;

namespace FairDispatchConsole
{
    class Worker
    {
        public static void Main2()
        {
            TestUtilitiesClass.PrintThreadId("Worker.Main2 (Consumer)");
            ConnectionFactory factory = TestUtilitiesClass.GetConnectionFactory();
            IConnection connection = factory.CreateConnection();
            IModel channel = connection.CreateModel();
            channel.QueueDeclare(queue: "task_queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            Console.WriteLine("[Worker] Waiting for messages.");

            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                byte[] body = ea.Body;
                RabbitMqMessage rabbitMqMessage = TestUtilitiesClass.ParseMessage(body);

                Console.WriteLine("[Worker] Received {0} ThreadId: {1}", rabbitMqMessage.ToJsonString(), Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(TimeSpan.FromSeconds(1));
                Console.WriteLine("[Worker] Done");
                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };

            channel.BasicConsume(queue: "task_queue",
                autoAck: false,
                consumer: consumer);
        }
    }
}
