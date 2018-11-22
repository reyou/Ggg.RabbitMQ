using System;
using System.Text;
using RabbitMQ.Client;
using TestUtilities;

namespace Producer
{
    class Send
    {
        public static void Main()
        {
            SendMessage(500);
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }

        private static void SendMessage(int count)
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
                    for (int i = 0; i < count; i++)
                    {
                        string message = "Publisher Message " + i;
                        byte[] body = Encoding.UTF8.GetBytes(message);

                        channel.BasicPublish(exchange: "", routingKey: "hello", basicProperties: null, body: body);

                        Console.WriteLine(" [x] Sent {0}", message);
                    }
                }
            }

        }
    }
}