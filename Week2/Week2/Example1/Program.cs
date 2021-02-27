using System;
using System.IO;

namespace Example1
{
    class Program
    {
        static void Main(string[] args)
        {
            F4();
        }

        static void PrintFolderInfo (string path, string prefix = "")
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            try
            {
                foreach (FileSystemInfo fi in dir.GetFileSystemInfos())
                {
                    Console.WriteLine(prefix + " " + fi.Name);
                    if (fi.GetType() == typeof(DirectoryInfo))
                    {
                        PrintFolderInfo(fi.FullName, prefix + "--");
                    }
                }
            } catch (Exception)
            {
                 
            }
        }
        private static void F4()
        {
            PrintFolderInfo(@"C:\Program Files\Java\jdk-15.0.2\include");
        }
        private static void F3()
        {
            DirectoryInfo dir = new DirectoryInfo(@"C:\Program Files\Java\jdk-15.0.2\include");

            foreach (FileSystemInfo fi in dir.GetFileSystemInfos())
            {
                Console.WriteLine(fi.Name);
            }
        }
        private static void F2()
        {
            DirectoryInfo dir = new DirectoryInfo(@"C:\Program Files\Java\jdk-15.0.2\include");

            foreach (DirectoryInfo d in dir.GetDirectories())
            {
                Console.WriteLine(d.Name);
            }

            foreach (FileInfo f in dir.GetFiles())
            {
                Console.WriteLine(f.Name);
            }
        }

        private static void F1 ()
        {
            DirectoryInfo dir = new DirectoryInfo(@"C:\");

            foreach (DirectoryInfo d in dir.GetDirectories())
            {
                Console.WriteLine(d.Name);
            }
        }

        private static void E ()
        {
            foreach (string l in Environment.GetLogicalDrives())
            {
                Console.WriteLine(l);
            }
        }
    }
}
