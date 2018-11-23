using System;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using TestUtilities;

namespace MessageAcknowledgmentConsole
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
                        channel.QueueDeclare(queue: "MessageAcknowledgment",
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);
                        for (int i = 0; i < count; i++)
                        {
                            byte[] body = TestUtilitiesClass.GetMessage(i.ToString());
                            RabbitMqMessage item = TestUtilitiesClass.ParseMessage(body);
                            channel.BasicPublish(exchange: "",
                                routingKey: "MessageAcknowledgment",
                                basicProperties: properties,
                                body: body);
                            Console.WriteLine("Queued {0}", item.ToJsonString());
                            Console.WriteLine("");
                            Thread.Sleep(TimeSpan.FromSeconds(0.5));
                        }

                    }
                }


            };
            taskFactory.StartNew(action);
        }
    }
}