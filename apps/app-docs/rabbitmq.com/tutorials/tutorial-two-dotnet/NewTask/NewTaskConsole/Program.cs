using System;
using System.ComponentModel;

namespace NewTaskConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            int selection = 1;
            switch (selection)
            {
                case 0:
                    Worker.Main2();
                    break;
                case 1:
                    Receive.Main2();
                    break;
            }
            Console.WriteLine("finish");
            Console.ReadLine();
        }
    }
}
