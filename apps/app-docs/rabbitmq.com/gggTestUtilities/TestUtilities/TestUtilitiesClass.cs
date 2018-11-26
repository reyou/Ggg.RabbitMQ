using System;
using System.Text;
using System.Threading;
using RabbitMQ.Client;

namespace TestUtilities
{
    public class TestUtilitiesClass
    {

        public static ConnectionFactory GetConnectionFactory()
        {
            ConnectionFactory factory = new ConnectionFactory()
            {
                HostName = "192.168.1.136",
                UserName = "admin",
                Password = "password"
            };
            return factory;
        }


        public static byte[] GetMessage(string message = "")
        {
            message = $"Queue Message: {message}";
            RabbitMqMessage rabbitMqMessage = new RabbitMqMessage(message);
            return rabbitMqMessage.ToByteArray();
        }

        public static RabbitMqMessage ParseMessage(byte[] body)
        {
            RabbitMqMessage message = RabbitMqMessage.FromByteArray(body);
            return message;
        }


        public static void PrintThreadId(string messageTitle)
        {
            Console.WriteLine($"{messageTitle} ThreadId: " + Thread.CurrentThread.ManagedThreadId);
        }

        public static void SleepProducer()
        {
            Thread.Sleep(TimeSpan.FromSeconds(0.5));
        }

        public static void SleepConsumer()
        {
            Thread.Sleep(TimeSpan.FromSeconds(1));
        }

        public static void WriteLine(string message)
        {
            Console.WriteLine();
            StringBuilder builder = new StringBuilder();
            builder.Append(message);
            builder.Append(" ");
            builder.Append("ThreadId: " + Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine(builder.ToString());
            Console.WriteLine();
        }
    }
}
