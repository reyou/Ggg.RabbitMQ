using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using UtilitiesClassLibrary;

namespace Send.SenderSample
{
    // https://www.rabbitmq.com/tutorials/tutorial-one-dotnet.html
    public class MessageSender
    {
        public string Exchange { get; set; }
        public string QueueName { get; set; }
        public bool AutoDelete { get; set; }
        public bool Exclusive { get; set; }
        public bool IsDurable { get; set; }
        public string RoutingKey { get; set; }
        public IDictionary<string, object> Arguments { get; set; }
        public MessageSender(string exchange, string queueName, IDictionary<string, object> arguments = null,
            bool autoDelete = false,
            bool exclusive = false,
            bool isDurable = false,
            string routingKey = "")
        {
            QueueName = queueName;
            IsDurable = isDurable;
            Exclusive = exclusive;
            AutoDelete = autoDelete;
            Arguments = arguments;
            RoutingKey = routingKey;
            Exchange = exchange;
        }



        public void Send(dynamic message)
        {
            try
            {
                /*Main entry point to the RabbitMQ .NET AMQP client*/
                ConnectionFactory factory = new ConnectionFactory()
                {
                    HostName = GggUtilities.HostName
                };
                /*Create a connection to one of the endpoints provided by the IEndpointResolver
                 returned by the EndpointResolverFactory. By default the configured
                 hostname and port are used.*/
                using (IConnection connection = factory.CreateConnection())
                {
                    // Create and return a fresh channel, session, and model.
                    using (IModel channel = connection.CreateModel())
                    {
                        /*Declaring a queue is idempotent - it will only be created
                         if it doesn't exist already.*/
                        channel.QueueDeclare(queue: QueueName,
                            durable: IsDurable,
                            exclusive: Exclusive,
                            autoDelete: AutoDelete,
                            arguments: Arguments);
                        IBasicProperties basicProperties = null;
                        if (IsDurable)
                        {
                            /*At this point we're sure that the task_queue queue won't be lost even
                             if RabbitMQ restarts. Now we need to mark our messages as persistent - 
                             by setting IBasicProperties.SetPersistent to true.*/
                            basicProperties = channel.CreateBasicProperties();
                            basicProperties.Persistent = true;
                        }
                        /*The message content is a byte array, so you can encode
                         whatever you like there.*/
                        byte[] body = GggUtilities.DynamicToByteArray(message);
                        /*BasicPublish(this IModel model, string exchange,
                         string routingKey, 
                         IBasicProperties basicProperties, byte[] body)*/
                        channel.BasicPublish(exchange: Exchange,
                            routingKey: RoutingKey,
                            basicProperties: basicProperties,
                            body: body);
                        string messageString = message.ToString();
                        GggUtilities.WriteLog(this, "Exchange: " + Exchange + " "
                                                    + "Queue: " + QueueName + " "
                                                    + "RoutingKey: " + RoutingKey);
                        GggUtilities.WriteLog(this, "Sent: " + messageString);
                    }
                }
            }
            catch (Exception e)
            {
                GggUtilities.WriteLog(this, e);
                throw;
            }
        }




        public void SendMulti(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var foo = new
                {
                    i = i.ToString(),
                    guid = Guid.NewGuid().ToString(),
                    date = DateTime.Now.ToString("G")
                };
                Send(foo);
            }
        }
    }
}
