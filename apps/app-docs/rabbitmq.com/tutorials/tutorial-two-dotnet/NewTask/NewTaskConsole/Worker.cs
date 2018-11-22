using System;
using System.Text;
using RabbitMQ.Client;
using TestUtilities;

namespace NewTaskConsole
{
    internal class Worker
    {
        public static void Main2()
        {
            string[] args = { "a", "b", "c" };
            string message = GetMessage(args);
            byte[] body = Encoding.UTF8.GetBytes(message);
            ConnectionFactory factory = TestUtilitiesClass.GetConnectionFactory();
            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    IBasicProperties properties = channel.CreateBasicProperties();
                    properties.Persistent = true;
                    channel.QueueDeclare(queue: "task_queue",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);
                    channel.BasicPublish(exchange: "",
                        routingKey: "task_queue",
                        basicProperties: properties,
                        body: body);
                    Console.WriteLine(" [x] Worker-Sent {0}", body);

                }
            }
        }

        private static string GetMessage(string[] strings)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < strings.Length; i++)
            {
                builder.Append(strings[i]);
            }
            return builder.ToString();
        }


    }
}