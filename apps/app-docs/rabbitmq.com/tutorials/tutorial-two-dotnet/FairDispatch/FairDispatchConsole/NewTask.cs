using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using RabbitMQ.Client;
using TestUtilities;

namespace FairDispatchConsole
{
    public class NewTask
    {
        public static void Main2(int count)
        {
            TestUtilitiesClass.PrintThreadId("NewTask.Main2 (Producer)");
            ConnectionFactory factory = TestUtilitiesClass.GetConnectionFactory();
            using (IConnection connection = factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "task_queue",
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                byte[] message = TestUtilitiesClass.GetMessage("FairDispatchConsole");

                IBasicProperties properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                for (int i = 0; i < count; i++)
                {
                    channel.BasicPublish(exchange: "",
                        routingKey: "task_queue",
                        basicProperties: properties,
                        body: message);
                    Console.WriteLine("[FairDispatchConsole] Sent {0} ThreadId: {1}", message, Thread.CurrentThread.ManagedThreadId);
                    Thread.Sleep(TimeSpan.FromSeconds(0.5));
                }
            }
        }
    }
}
