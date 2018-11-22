using System;
using System.Text;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using TestUtilities;

namespace Receiver.autoAckFalse
{
    partial class Receive
    {
        public static void Main2()
        {
            ConnectionFactory factory = TestUtilitiesClass.GetConnectionFactory();
            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "hello",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
                    int counter = 1;
                    consumer.Received += (model, ea) =>
                    {
                        byte[] body = ea.Body;
                        string message = Encoding.UTF8.GetString(body);
                        Console.WriteLine(" [x-con1] Received {0}, counter: {1}, threadId: {2}", message, counter, Thread.CurrentThread.ManagedThreadId);
                        counter++;
                    };

                    channel.BasicConsume(queue: "hello", autoAck: false, consumer: consumer);

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }
    }
}