using System;
using TestUtilities;

namespace RpcClientInterfaceConsole
{
    public class Rpc
    {
        public static void Main2()
        {
            RpcClient rpcClient = new RpcClient();
            TestUtilitiesClass.WriteLine("Rpc Main2 Requesting fib(30)");
            string response = rpcClient.Call("30");
            Console.WriteLine("Rpc Main2 Got '{0}'", response);
            rpcClient.Close();
        }
    }
}