using System.Threading.Tasks;

namespace RoundRobinDispatchingConsole
{
    internal class MessageReader3
    {
        public static void Read()
        {
            TaskFactory factory = new TaskFactory();
            factory.StartNew(() => { MessageReaderCommon.HandleMessage("MessageReader3"); });
        }
    };
}