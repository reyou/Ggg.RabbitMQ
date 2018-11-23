using System.Threading.Tasks;

namespace MessageAcknowledgmentConsole
{
    internal class MessageReader
    {
        public static void Read()
        {
            TaskFactory factory = new TaskFactory();
            factory.StartNew(() =>
            {
                MessageReaderCommon.HandleMessage("MessageReader1");
            });
        }
    }
}