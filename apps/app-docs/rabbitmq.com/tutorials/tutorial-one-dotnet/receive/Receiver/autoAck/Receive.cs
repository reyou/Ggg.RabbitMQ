using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using TestUtilities;

namespace Receiver.autoAck
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

                    consumer.Received += (model, ea) =>
                    {
                        byte[] body = ea.Body;
                        string message = Encoding.UTF8.GetString(body);
                        Console.WriteLine(" [x-con1] Received {0}", message);
                    };

                    channel.BasicConsume(queue: "hello", autoAck: true, consumer: consumer);

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }
    }
}