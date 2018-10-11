using Microsoft.VisualStudio.TestTools.UnitTesting;
using Send.SenderSample;
using UtilitiesClassLibrary;

namespace FuncTests
{
    /// <summary>
    /// <see cref="MessageSender"/>
    /// </summary>
    [TestClass]
    public class SenderTests
    {
        [TestMethod]
        public void SendTest()
        {
            MessageSender sender = new MessageSender(GggUtilities.ExchangeDefault, GggUtilities.QueueHello);
            sender.SendMulti(100);
        }

        [TestMethod]
        public void WorkMessageSenderTest()
        {
            MessageSender sender = new MessageSender(GggUtilities.ExchangeDefault, GggUtilities.QueueHello);
            sender.SendMulti(100);
            GggUtilities.ShowLog();
        }
    }
}
