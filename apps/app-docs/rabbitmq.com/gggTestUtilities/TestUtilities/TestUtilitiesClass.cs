using System;
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
    }
}
