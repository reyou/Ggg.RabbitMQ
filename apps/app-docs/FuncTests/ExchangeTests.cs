using Microsoft.VisualStudio.TestTools.UnitTesting;
using Receive.ReceiverSample;
using Send.SenderSample;
using System;
using UtilitiesClassLibrary;

namespace FuncTests
{
    [TestClass]
    public class ExchangeTests
    {

        [TestMethod]
        public void PublishReadExchangeTest()
        {
            // timers
            double seconds = 3;
            double waitTime = TimeSpan.FromSeconds(seconds).TotalMilliseconds;
            // sent
            MessageSender sender = new MessageSender(GggUtilities.ExchangeLogs, GggUtilities.QueueHello);
            sender.SendMulti(5);
            // wait
            GggUtilities.WriteLog(this, "Waiting {0} seconds before receiver.".Replace("{0}", seconds.ToString()));
            System.Threading.Thread.Sleep((int)waitTime);
            // receiver
            MessageReceiver receiver = new MessageReceiver("receiver1", GggUtilities.QueueHello);
            receiver.Receive();
            // wait
            GggUtilities.WriteLog(this, "Waiting {0} seconds for thread to finish.".Replace("{0}", seconds.ToString()));
            System.Threading.Thread.Sleep((int)waitTime);
            GggUtilities.ShowLog();
        }

        /// <summary>
        /// publish to our named exchange
        /// </summary>
        [TestMethod]
        public void PublishToExchangeTest()
        {
            MessageSender sender = new MessageSender(GggUtilities.ExchangeLogs, GggUtilities.QueueHello);
            sender.SendMulti(5);
            GggUtilities.ShowLog();
        }

        [TestMethod]
        public void BindQueueTest()
        {
            GggRabbitMq.BindQueue(GggUtilities.ExchangeLogs, GggUtilities.QueueHello);
            GggUtilities.ShowLog();
        }

        [TestMethod]
        public void DeclareQueueTest()
        {
            string declareQueue = GggRabbitMq.DeclareQueue();
            GggUtilities.WriteLog(this, "declareQueue: " + declareQueue);
            GggUtilities.ShowLog();
        }

        [TestMethod]
        public void DeclareExchangeTest()
        {
            GggRabbitMq.DeclareExchange(exchange: GggUtilities.ExchangeLogs, type: "fanout");
            GggUtilities.ShowLog();
        }
    }
}
