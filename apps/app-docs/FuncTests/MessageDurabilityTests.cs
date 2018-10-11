using Microsoft.VisualStudio.TestTools.UnitTesting;
using Receive.ReceiverSample;
using Send.SenderSample;
using System;
using UtilitiesClassLibrary;

namespace FuncTests
{
    /// <summary>
    /// http://localhost:15672/#/queues
    /// https://www.rabbitmq.com/tutorials/tutorial-two-dotnet.html
    /// </summary>
    [TestClass]
    public class MessageDurabilityTests
    {

        [TestMethod]
        public void MessageDurabilityTest()
        {
            // timers
            double seconds = 3;
            double waitTime = TimeSpan.FromSeconds(seconds).TotalMilliseconds;
            // sent
            MessageSender sender = new MessageSender(GggUtilities.QueueDurable, null,
                autoDelete: false, exclusive: false, isDurable: true,
                routingKey: GggUtilities.QueueDurable);
            sender.SendMulti(10);
            // wait
            GggUtilities.WriteLog(this, "Waiting {0} seconds before receiver.".Replace("{0}", seconds.ToString()));
            System.Threading.Thread.Sleep((int)waitTime);
            // receiver
            MessageReceiver receiver = new MessageReceiver("receiver1", GggUtilities.QueueDurable, null, autoDelete: false, exclusive: false, isDurable: true);
            receiver.Receive();
            // wait
            GggUtilities.WriteLog(this, "Waiting {0} seconds for thread to finish.".Replace("{0}", seconds.ToString()));
            System.Threading.Thread.Sleep((int)waitTime);
            GggUtilities.ShowLog();
        }

        [TestMethod]
        public void MessageDurabilitySendTest()
        {
            // sent
            MessageSender sender = new MessageSender(GggUtilities.QueueDurable, null,
                autoDelete: false, exclusive: false,
                isDurable: true,
                routingKey: GggUtilities.QueueDurable);
            sender.SendMulti(10);
            GggUtilities.ShowLog();
        }
    }
}
