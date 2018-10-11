using Microsoft.VisualStudio.TestTools.UnitTesting;
using Receive.ReceiverSample;
using Send.SenderSample;
using System;
using UtilitiesClassLibrary;

namespace FuncTests
{
    /// <summary>
    /// https://www.rabbitmq.com/tutorials/tutorial-two-dotnet.html
    /// In order to change this behavior we can use the basicQos method with the 
    /// prefetchCount = 1 setting. This tells RabbitMQ not to give more than one 
    /// message to a worker at a time. Or, in other words, don't dispatch a new 
    /// message to a worker until it has processed and acknowledged the previous one. 
    /// Instead, it will dispatch it to the next worker that is not still busy.
    /// If all the workers are busy, your queue can fill up. You will want to 
    /// keep an eye on that, and maybe add more workers, or have some other strategy.
    /// </summary>
    [TestClass]
    public class BasicQosTests
    {
        [TestMethod]
        public void BasicQosTest()
        {
            // timers
            double seconds = 3;
            double waitTime = TimeSpan.FromSeconds(seconds).TotalMilliseconds;
            // sent
            MessageSender sender = new MessageSender(GggUtilities.QueueHello, null,
                autoDelete: false, exclusive: false, isDurable: false,
                routingKey: GggUtilities.QueueHello);
            sender.SendMulti(10);
            // wait
            GggUtilities.WriteLog(this, "Waiting {0} seconds before receiver.".Replace("{0}", seconds.ToString()));
            System.Threading.Thread.Sleep((int)waitTime);
            // receiver
            MessageReceiver receiver = new MessageReceiver("receiver1", GggUtilities.QueueHello, null,
                autoDelete: false, exclusive: false, isDurable: false, basicQosEnabled: true);
            receiver.Receive();
            // wait
            GggUtilities.WriteLog(this, "Waiting {0} seconds for thread to finish.".Replace("{0}", seconds.ToString()));
            System.Threading.Thread.Sleep((int)waitTime);
            GggUtilities.ShowLog();
        }
    }
}
