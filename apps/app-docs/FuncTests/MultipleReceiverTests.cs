using Microsoft.VisualStudio.TestTools.UnitTesting;
using Receive.ReceiverSample;
using Send.SenderSample;
using System;
using System.Diagnostics;
using UtilitiesClassLibrary;

namespace FuncTests
{
    [TestClass]
    public class MultipleReceiverTests
    {

        [TestMethod]
        public void RoundRobinSample()
        {
            double seconds = 3;
            double waitTime = TimeSpan.FromSeconds(seconds).TotalMilliseconds;
            // set receivers
            MessageReceiver receiver1 = new MessageReceiver("receiver1", GggUtilities.QueueHello);
            MessageReceiver receiver2 = new MessageReceiver("receiver2", GggUtilities.QueueHello);
            receiver1.Receive();
            receiver2.Receive();
            // send messages
            MessageSender sender = new MessageSender(GggUtilities.ExchangeDefault, GggUtilities.QueueHello);
            sender.SendMulti(100);
            // wait
            Debug.WriteLine("Waiting {0} seconds for thread to finish.", seconds);
            System.Threading.Thread.Sleep((int)waitTime);
            GggUtilities.ShowLog();
        }
    }
}
