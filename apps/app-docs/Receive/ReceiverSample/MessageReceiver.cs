using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using UtilitiesClassLibrary;

namespace Receive.ReceiverSample
{
    public class MessageReceiver
    {

        public bool AutoAck { get; set; }
        public string QueueName { get; set; }
        public bool AutoDelete { get; set; }
        public bool Exclusive { get; set; }
        public bool IsDurable { get; set; }
        public bool BasicQosEnabled { get; set; }
        public IModel Channel { get; set; }
        public IDictionary<string, object> Arguments { get; set; }
        public string Name { get; set; }
        public MessageReceiver(string name,
            string queueName, IDictionary<string, object> arguments = null,
            bool autoDelete = false,
            bool exclusive = false,
            bool isDurable = false,
            bool basicQosEnabled = false,
            bool autoAck = true)
        {
            Name = name;
            QueueName = queueName;
            IsDurable = isDurable;
            Exclusive = exclusive;
            AutoDelete = autoDelete;
            Arguments = arguments;
            BasicQosEnabled = basicQosEnabled;
            AutoAck = autoAck;
        }

        public void Receive()
        {
            try
            {
                ConnectionFactory factory = new ConnectionFactory()
                {
                    HostName = GggUtilities.HostName
                };
                // consumer.Received event not firing when channel.BasicConsume is called
                // https://groups.google.com/forum/#!topic/rabbitmq-users/l0GQ4w3sYEU
                IConnection connection = factory.CreateConnection();
                IModel channel = connection.CreateModel();
                Channel = channel;
                /*Declaring a queue is idempotent - it will only be created
                 if it doesn't exist already.*/
                channel.QueueDeclare(queue: QueueName,
                    durable: IsDurable,
                    exclusive: Exclusive,
                    autoDelete: AutoDelete,
                    arguments: Arguments);

                // https://www.rabbitmq.com/tutorials/tutorial-two-dotnet.html
                /*we can use the basicQos method with the prefetchCount = 1 setting.
                 This tells RabbitMQ not to give more than one message to a worker at a time. 
                 Or, in other words, don't dispatch a new message to a worker until it has processed 
                 and acknowledged the previous one. Instead, it will dispatch it to the next 
                 worker that is not still busy.*/
                if (BasicQosEnabled)
                {
                    channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
                }


                /*Experimental class exposing an IBasicConsumer's
            methods as separate events.*/
                EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
                consumer.Received += OnConsumerOnReceived;
                /*Start a Basic content-class consumer*/
                /*Manual message acknowledgments are turned on by default.
                 In previous examples we explicitly turned them off by setting the autoAck 
                 ("automatic acknowledgement mode") parameter to true. It's time to remove 
                 this flag and manually send a proper acknowledgment from the worker, 
                 once we're done with a task.*/
                string logMessage = Name + " " + "AutoAck is " + AutoAck;
                GggUtilities.WriteLog(this, logMessage);
                channel.BasicConsume(queue: QueueName,
                    autoAck: AutoAck,
                    consumer: consumer);

            }
            catch (Exception e)
            {
                GggUtilities.WriteLog(this, e);
                throw;
            }
        }



        private void OnConsumerOnReceived(object model, BasicDeliverEventArgs ea)
        {
            byte[] body = ea.Body;
            string message = Encoding.UTF8.GetString(body);
            // https://www.rabbitmq.com/tutorials/tutorial-two-dotnet.html
            if (AutoAck == false)
            {
                /*Using this code we can be sure that even if you kill a worker using
                 CTRL+C while it was processing a message, nothing will be lost. Soon after 
                 the worker dies all unacknowledged messages will be redelivered.*/
                Channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                GggUtilities.WriteLog(this, "OnConsumerOnReceived.Channel.BasicAck.deliveryTag: " + ea.DeliveryTag);
            }
            string logMessage = Name + " " + "Received: " + message;
            GggUtilities.WriteLog(this, "Queue: " + QueueName);
            GggUtilities.WriteLog(this, logMessage);
        }
    }
}