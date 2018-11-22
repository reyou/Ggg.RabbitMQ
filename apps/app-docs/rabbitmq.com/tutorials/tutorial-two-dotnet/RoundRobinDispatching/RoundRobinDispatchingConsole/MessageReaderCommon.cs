using System;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using TestUtilities;

namespace RoundRobinDispatchingConsole
{
    internal class MessageReaderCommon
    {
        // consumer.Received event not firing when channel.BasicConsume is called
        // https://groups.google.com/forum/#!topic/rabbitmq-users/l0GQ4w3sYEU
        public static void HandleMessage(string readerName)
        {
            ConnectionFactory factory = TestUtilitiesClass.GetConnectionFactory();
            IConnection connection = factory.CreateConnection();
            IModel channel = connection.CreateModel();
            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                byte[] body = ea.Body;
                RabbitMqMessage message = TestUtilitiesClass.ParseMessage(body);
                Console.WriteLine($"[{readerName}] Received {message.ToJsonString()}");
                Console.WriteLine("");
                Thread.Sleep(TimeSpan.FromSeconds(1));
            };
            Console.WriteLine($"BasicConsume: {readerName} ThreadId: " + Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("");
            channel.BasicConsume(queue: "round_robin", autoAck: true, consumer: consumer);

        }
    }
}