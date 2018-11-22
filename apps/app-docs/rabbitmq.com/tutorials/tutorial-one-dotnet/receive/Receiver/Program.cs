namespace Receiver
{
    class Program
    {
        public static void Main()
        {
            int selection = 2;
            switch (selection)
            {
                case 0:
                    autoAck.Receive.Main2();
                    break;
                case 1:
                    autoAckFalse.Receive.Main2();
                    break;
                case 2:
                    multiReceiver.Receive.Main2();
                    break;

            }
        }
    }
}
