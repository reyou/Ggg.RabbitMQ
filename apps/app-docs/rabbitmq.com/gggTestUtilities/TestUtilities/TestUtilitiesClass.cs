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


    }
}
