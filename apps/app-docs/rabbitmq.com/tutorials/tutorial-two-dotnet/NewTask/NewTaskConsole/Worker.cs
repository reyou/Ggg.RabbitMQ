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

                    channel.BasicPublish(exchange: "",
                        routingKey: "task_queue",
                        basicProperties: properties,
                        body: body);

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