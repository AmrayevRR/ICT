using System;
using System.Collections.Generic;
using System.IO;

namespace Example2
{
    class Layer
    {
        public DirectoryInfo dir
        {
            get;
            set;
        }
        public int pos
        {
            get;
            set;
        }
        public FileInfo file 
        {
            get;
            set;
        }
        public List<FileSystemInfo> content
        {
            get;
            set;
        }
        public bool showCreationTime = false;
        public bool showLastUpdateTime = false;

        public Layer(DirectoryInfo dir, int pos)
        {
            this.dir = dir;
            this.pos = pos;
            this.content = new List<FileSystemInfo>();


            content.AddRange(this.dir.GetDirectories());
            content.AddRange(this.dir.GetFiles());
        }
        public Layer(FileInfo file, int pos)
        {
            this.file = file;
        }

        long GetDirectoryLength(DirectoryInfo directory)
        {
            long length = 0;

            foreach (DirectoryInfo d in directory.GetDirectories())
                length += GetDirectoryLength(d);

            foreach (FileInfo f in directory.GetFiles())
                length += f.Length;

            return length;
        }

        public void PrintInfo()
        {
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.Clear();
            string output;

            //Console.ForegroundColor = ConsoleColor.White;
            int cnt = 0;
            foreach (DirectoryInfo d in dir.GetDirectories())
            {
                output = d.Name + "    " + GetDirectoryLength(d) + " bytes";

                if (cnt == pos)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Blue;

                    if (showCreationTime)
                        output += "    Creation time: " + d.CreationTime;
                    if (showLastUpdateTime)
                        output += "    Last update time: " + d.LastWriteTime;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.BackgroundColor = ConsoleColor.Cyan;
                }
                Console.WriteLine(output);
                cnt++;
            }
            //Console.ForegroundColor = ConsoleColor.Red;
            foreach (FileInfo f in dir.GetFiles())
            {
                output = f.Name + "    " + f.Length + " bytes";

                if (cnt == pos)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.BackgroundColor = ConsoleColor.Blue;

                    if (showCreationTime)
                        output += "    Creation time: " + f.CreationTime;
                    if (showLastUpdateTime)
                        output += "    Last update time: " + f.LastWriteTime;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.BackgroundColor = ConsoleColor.Cyan;
                }
                Console.WriteLine(output);
                cnt++;
            }
        }

        public void PrintFileContent()
        {
            Console.Clear();

            string[] lines = File.ReadAllLines(file.FullName);
            foreach(string line in lines) 
            {
                Console.WriteLine(line);
            }
        }

        public FileSystemInfo GetCurrentObject()
        {
            return content[pos];
        }

        public void SetNewPosition(int d)
        {
            if (d > 0)
            {
                pos++;
            }
            else
            {
                pos--;
            }
            if (pos >= content.Count)
            {
                pos = 0;
            }
            else if (pos < 0)
            {
                pos = content.Count - 1;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            F3();
        }

        private static void F3()
        {

            Stack<Layer> history = new Stack<Layer>();
            history.Push(new Layer(new DirectoryInfo(@"C:\Users\Амраева Карина\source\repos\ict"), 0));

            bool escape = false;
            bool fileIsSelected = false;

            while (!escape)
            {
                Console.Clear();

                if (fileIsSelected)
                    history.Peek().PrintFileContent();
                else
                    history.Peek().PrintInfo();

                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);

                switch (consoleKeyInfo.Key)
                {
                    case ConsoleKey.Enter:
                        history.Peek().showCreationTime = false;
                        history.Peek().showLastUpdateTime = false;
                        if (history.Peek().GetCurrentObject().GetType() == typeof(DirectoryInfo))
                        {
                            history.Push(new Layer(history.Peek().GetCurrentObject() as DirectoryInfo, 0));
                        }
                        else
                        {
                            history.Push(new Layer(history.Peek().GetCurrentObject() as FileInfo, 0));
                            fileIsSelected = true;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        history.Peek().showCreationTime = false;
                        history.Peek().showLastUpdateTime = false;
                        history.Peek().SetNewPosition(-1);
                        break;
                    case ConsoleKey.DownArrow:
                        history.Peek().showCreationTime = false;
                        history.Peek().showLastUpdateTime = false;
                        history.Peek().SetNewPosition(1);
                        break;
                    case ConsoleKey.Escape:
                        history.Peek().showCreationTime = false;
                        history.Peek().showLastUpdateTime = false;
                        history.Pop();
                        fileIsSelected = false;
                        break;
                    case ConsoleKey.T:
                        history.Peek().showCreationTime = !history.Peek().showCreationTime;
                        break;
                    case ConsoleKey.U:
                        history.Peek().showLastUpdateTime = !history.Peek().showLastUpdateTime;
                        break;

                }
            }
        }

        private static void F2()
        {
            while (true)
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