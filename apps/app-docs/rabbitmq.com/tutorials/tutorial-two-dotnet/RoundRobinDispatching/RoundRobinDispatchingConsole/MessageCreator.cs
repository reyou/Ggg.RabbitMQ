using System;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using TestUtilities;

namespace RoundRobinDispatchingConsole
{
    internal class MessageCreator
    {
        public static void Start(int count)
        {
            TaskFactory taskFactory = new TaskFactory();
            Action action = () =>
            {
                ConnectionFactory factory = TestUtilitiesClass.GetConnectionFactory();
                using (IConnection connection = factory.CreateConnection())
                {
                    using (IModel channel = connection.CreateModel())
                    {
                        IBasicProperties properties = channel.CreateBasicProperties();
                        properties.Persistent = true;
                        channel.QueueDeclare(queue: "round_robin",
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);
                        for (int i = 0; i < count; i++)
                        {
                            byte[] body = TestUtilitiesClass.GetMessage(i.ToString());
                            RabbitMqMessage item = TestUtilitiesClass.ParseMessage(body);
                            channel.BasicPublish(exchange: "",
                                routingKey: "round_robin",
                                basicProperties: properties,
                                body: body);
                            Console.WriteLine("Worker-Sent {0}", item.ToJsonString());
                            Console.WriteLine("");
                            Thread.Sleep(TimeSpan.FromSeconds(1));
                        }

                    }
                }


            };
            taskFactory.StartNew(action);
        }
    }
}