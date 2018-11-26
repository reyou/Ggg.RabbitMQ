using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using TestUtilities;

namespace RpcClientInterfaceConsole
{
    class RpcServer
    {
        public static void Main2()
        {
            TestUtilitiesClass.WriteLine("RpcServer Main2 started.");
            ConnectionFactory factory = TestUtilitiesClass.GetConnectionFactory();

            IConnection connection = factory.CreateConnection();
            IModel channel = connection.CreateModel();

            TestUtilitiesClass.WriteLine("RpcServer Main2 QueueDeclare: rpc_queue");

            channel.QueueDeclare(queue: "rpc_queue",
              durable: false,
              exclusive: false,
              autoDelete: false,
              arguments: null);

            channel.BasicQos(0, 1, false);

            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                TestUtilitiesClass.WriteLine("RpcServer Main2 Received message");
                string response = null;

                byte[] body = ea.Body;
                IBasicProperties props = ea.BasicProperties;
                IBasicProperties replyProps = channel.CreateBasicProperties();
                replyProps.CorrelationId = props.CorrelationId;
                TestUtilitiesClass.WriteLine("RpcServer Main2 CorrelationId: " + props.CorrelationId);

                try
                {
                    string message = Encoding.UTF8.GetString(body);
                    int n = int.Parse(message);
                    Console.WriteLine("RpcServer Main2 fib({0})", message);
                    response = fib(n).ToString();
                }
                catch (Exception e)
                {
                    TestUtilitiesClass.WriteLine("RpcServer Main2 Exception " + e.Message);
                    response = "";
                }
                finally
                {
                    TestUtilitiesClass.WriteLine("RpcServer Main2 finally");
                    byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                    TestUtilitiesClass.WriteLine("RpcServer Main2 finally BasicPublish routingKey: " + props.ReplyTo);
                    channel.BasicPublish(exchange: "", routingKey: props.ReplyTo,
                      basicProperties: replyProps, body: responseBytes);
                    channel.BasicAck(deliveryTag: ea.DeliveryTag,
                      multiple: false);
                }
            };
            TestUtilitiesClass.WriteLine("RpcServer Main2 BasicConsume queue: rpc_queue");
            channel.BasicConsume(queue: "rpc_queue",
                autoAck: false,
                consumer: consumer);
            Console.WriteLine("RpcServer Main2 Awaiting RPC requests");

        }

        /// 

        /// Assumes only valid positive integer input.
        /// Don't expect this one to work for big numbers, and it's
        /// probably the slowest recursive implementation possible.
        /// 
        private static int fib(int n)
        {
            TestUtilitiesClass.WriteLine("fib is called: " + n);
            if (n == 0 || n == 1)
            {
                return n;
            }

            return fib(n - 1) + fib(n - 2);
        }
    }

}
