using System;
using System.IO;

namespace Example3
{
    class Program
    {
        static void Main(string[] args)
        {
            F3();
        }
        private static void PrintInfo (string path, int pos) 
        {
            //Console.BackgroundColor = ConsoleColor.Blue;
            DirectoryInfo dir = new DirectoryInfo(path);
            Console.ForegroundColor = ConsoleColor.White;
            int cnt = 0;
            foreach (DirectoryInfo d in dir.GetDirectories())
            {
                if (cnt == pos)
                {
                    Console.BackgroundColor = ConsoleColor.Cyan;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                Console.WriteLine(d.Name);
                cnt++;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            foreach (FileInfo f in dir.GetFiles())
            {
                Console.WriteLine(f.Name);
            }
        }
        private static void F3()
        {
            int pos = 0;

            bool escape = false;
            
            while (!escape)
            {
                Console.Clear();
                PrintInfo(@"C:\Program Files\JetBrains\DataGrip 2020.2.2", pos);

                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);
                
                switch (consoleKeyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (pos > 0) pos--;
                        break;
                    case ConsoleKey.DownArrow:
                        pos++;
                        break;
                    case ConsoleKey.Escape:
                        escape = true;
                        break;
                }
            }
        }

        private static void F2()
        {
            while(true)
            {
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
                if (consoleKeyInfo.Key == ConsoleKey.Escape) break;
                Console.WriteLine(consoleKeyInfo.KeyChar);
            }
        }

        private static void F1()
        {
            ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
            Console.WriteLine(consoleKeyInfo);
        }
    }
}
