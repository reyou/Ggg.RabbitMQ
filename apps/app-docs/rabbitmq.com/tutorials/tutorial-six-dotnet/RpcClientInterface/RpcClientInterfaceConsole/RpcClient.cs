using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using TestUtilities;

namespace RpcClientInterfaceConsole
{
    public class RpcClient
    {
        private readonly IConnection connection;
        private readonly IModel channel;
        private readonly string replyQueueName;
        private readonly EventingBasicConsumer consumer;
        private readonly BlockingCollection<string> respQueue = new BlockingCollection<string>();
        private readonly IBasicProperties props;

        public RpcClient()
        {
            TestUtilitiesClass.WriteLine("RpcClient constructor started");
            ConnectionFactory factory = TestUtilitiesClass.GetConnectionFactory();

            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            replyQueueName = channel.QueueDeclare().QueueName;
            consumer = new EventingBasicConsumer(channel);
            TestUtilitiesClass.WriteLine("RpcClient replyQueueName: " + replyQueueName);
            props = channel.CreateBasicProperties();
            string correlationId = Guid.NewGuid().ToString();
            TestUtilitiesClass.WriteLine("RpcClient correlationId: " + correlationId);
            props.CorrelationId = correlationId;
            props.ReplyTo = replyQueueName;

            consumer.Received += (model, ea) =>
            {
                TestUtilitiesClass.WriteLine("RpcClient Received");
                byte[] body = ea.Body;
                string response = Encoding.UTF8.GetString(body);
                if (ea.BasicProperties.CorrelationId == correlationId)
                {
                    respQueue.Add(response);
                }
            };
        }

        public string Call(string message)
        {
            TestUtilitiesClass.WriteLine("RpcClient Call message: " + message);
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            TestUtilitiesClass.WriteLine("RpcClient BasicPublish routingKey: rpc_queue");
            channel.BasicPublish(
                exchange: "",
                routingKey: "rpc_queue",
                basicProperties: props,
                body: messageBytes);
            TestUtilitiesClass.WriteLine("RpcClient BasicConsume queue: " + replyQueueName);
            channel.BasicConsume(
                consumer: consumer,
                queue: replyQueueName,
                autoAck: true);

            return respQueue.Take();
        }

        public void Close()
        {
            TestUtilitiesClass.WriteLine("RpcClient Close");
            connection.Close();
        }
    }
}
