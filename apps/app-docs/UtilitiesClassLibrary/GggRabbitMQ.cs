using RabbitMQ.Client;

namespace UtilitiesClassLibrary
{
    public class GggRabbitMq
    {
        /*There are a few exchange types available: direct, topic, headers and fanout.*/
        /*The fanout exchange is very simple. As you can probably guess from the name,
         it just broadcasts all the messages it receives to all the queues it knows. 
         And that's exactly what we need for our logger.*/
        /// <summary>
        /// https://www.rabbitmq.com/tutorials/tutorial-three-dotnet.html
        /// </summary>
        /// <param name="exchange"></param>
        /// <param name="type"></param>
        public static void DeclareExchange(string exchange, string type)
        {
            ConnectionFactory factory = new ConnectionFactory()
            {
                HostName = GggUtilities.HostName
            };
            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange, type);
                }
            }
            GggUtilities.WriteLog(typeof(GggRabbitMq).FullName, string.Format("DeclareExchange: exchange: {0} type: {1}", exchange, type));
        }

        /// <summary>
        /// In the .NET client, when we supply no parameters to queueDeclare() 
        /// we create a non-durable, exclusive, autodelete queue with a generated name: 
        /// once we disconnect the consumer the queue should be automatically deleted.
        /// https://www.rabbitmq.com/tutorials/tutorial-three-dotnet.html
        /// </summary>
        public static string DeclareQueue()
        {
            ConnectionFactory factory = new ConnectionFactory()
            {
                HostName = GggUtilities.HostName
            };
            string queueName;
            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    queueName = channel.QueueDeclare().QueueName;
                    GggUtilities.WriteLog(typeof(GggRabbitMq).FullName, string.Format("DeclareQueue: queue created: {0}", queueName));
                }
            }
            GggUtilities.WriteLog(typeof(GggRabbitMq).FullName, string.Format("DeclareQueue: queue deleted: {0}", queueName));
            return queueName;
        }

        // https://www.rabbitmq.com/tutorials/tutorial-three-dotnet.html
        public static void BindQueue(string exchange, string queueName, string routingKey = "")
        {
            ConnectionFactory factory = new ConnectionFactory()
            {
                HostName = GggUtilities.HostName
            };

            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueBind(queue: queueName, exchange: exchange, routingKey: routingKey);
                }
            }
            GggUtilities.WriteLog(typeof(GggRabbitMq).FullName, string.Format("BindQueue: exchange-queue bind: {0}-{1}", exchange, queueName));
        }
    }
}
