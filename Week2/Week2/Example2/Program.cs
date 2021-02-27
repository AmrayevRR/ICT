using System;

namespace Example2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Yes");
        }

        private static void F1()
        {
            ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
            Console.WriteLine(consoleKeyInfo.KeyChar);
        }
    }
}
