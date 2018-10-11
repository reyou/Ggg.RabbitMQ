using Microsoft.VisualStudio.TestTools.UnitTesting;
using Receive.ReceiverSample;
using Send.SenderSample;
using System;
using System.Diagnostics;
using UtilitiesClassLibrary;

namespace FuncTests
{
    [TestClass]
    public class ReceiverTests
    {
        [TestMethod]
        public void PublishAndReceiveTest()
        {
            double seconds = 3;
            double waitTime = TimeSpan.FromSeconds(seconds).TotalMilliseconds;
            // send
            MessageSender sender = new MessageSender(GggUtilities.ExchangeDefault, GggUtilities.QueueHello);
            sender.SendMulti(100);
            // receive
            Debug.WriteLine("Waiting {0} seconds before receive.", seconds);
            System.Threading.Thread.Sleep((int)waitTime);
            MessageReceiver receiver = new MessageReceiver("receiver1", GggUtilities.QueueHello);
            receiver.Receive();
            Debug.WriteLine("Waiting {0} seconds for thread to finish.", seconds);
            System.Threading.Thread.Sleep((int)waitTime);
            GggUtilities.ShowLog();
        }

        [TestMethod]
        public void PublishAndReceiveWorkerTestTrueAct()
        {
            double seconds = 3;
            double waitTime = TimeSpan.FromSeconds(seconds).TotalMilliseconds;
            // send
            MessageSender sender = new MessageSender(GggUtilities.ExchangeDefault, GggUtilities.QueueHello);
            sender.SendMulti(10);
            // receive
            Debug.WriteLine("Waiting {0} seconds before receive.", seconds);
            System.Threading.Thread.Sleep((int)waitTime);
            MessageReceiver receiver = new MessageReceiver("receiver1", GggUtilities.QueueTaskQueue);
            receiver.Receive();
            Debug.WriteLine("Waiting {0} seconds for thread to finish.", seconds);
            System.Threading.Thread.Sleep((int)waitTime);
            GggUtilities.ShowLog();
        }

        [TestMethod]
        public void PublishAndReceiveWorkerTestFalseAct()
        {
            double seconds = 3;
            double waitTime = TimeSpan.FromSeconds(seconds).TotalMilliseconds;
            // send
            MessageSender sender = new MessageSender(GggUtilities.ExchangeDefault, GggUtilities.QueueHello);
            sender.SendMulti(10);
            // receive
            Debug.WriteLine("Waiting {0} seconds before receive.", seconds);
            System.Threading.Thread.Sleep((int)waitTime);
            MessageReceiver receiver = new MessageReceiver("receiver1", GggUtilities.QueueTaskQueue, arguments: null,
                autoDelete: false, exclusive: false, isDurable: false,
                basicQosEnabled: false, autoAck: false);
            receiver.Receive();
            Debug.WriteLine("Waiting {0} seconds for thread to finish.", seconds);
            System.Threading.Thread.Sleep((int)waitTime);
            GggUtilities.ShowLog();
        }

        [TestMethod]
        public void ReceiveTest()
        {
            MessageReceiver receiver = new MessageReceiver("receiver1", GggUtilities.QueueHello);
            receiver.Receive();
            double seconds = 5;
            double waitTime = TimeSpan.FromSeconds(seconds).TotalMilliseconds;
            Debug.WriteLine("Waiting {0} seconds for thread to finish.", seconds);
            System.Threading.Thread.Sleep((int)waitTime);
        }
    }
}
