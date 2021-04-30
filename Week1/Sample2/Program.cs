using System;

namespace Sample2
{
    class Program
    {
        static void Main(string[] args)
        {
            //string str = Console.ReadLine();
            //Console.WriteLine(Solution.defangIPaddr(str));

            //int num = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine(Solution1.numberOfSteps(num));

            /*int n = Convert.ToInt32(Console.ReadLine());
            string[] word1 = new string[n];
            string str;
            for (int i = 0; i < n; i++)
            {
                word1[i] = Convert.ToString(Console.ReadLine());
            }
            n = Convert.ToInt32(Console.ReadLine());
            string[] word2 = new string[n];
            for (int i = 0; i < n; i++)
            {
                word2[i] = Convert.ToString(Console.ReadLine());
            }
            Console.WriteLine(Solution2.ArrayStringAreEqual(word1, word2));*/

            Cat cat = new Cat(2);

            Console.WriteLine(cat.Size);

        }
    }

    class Solution
    {
        internal static string defangIPaddr(string address)
        {
            string res = "";
            foreach (char ch in address)
            {
                if (ch == '.')
                    res += "[.]";
                else
                    res += ch;
            }
            return res;
        }
    };

    class Solution1
    {
        internal static int numberOfSteps(int num)
        {
            int cnt = 0;
            while (num != 0)
            {
                if (num % 2 == 0)
                    num /= 2;
                else
                    num -= 1;
                cnt++;
            }
            return cnt;
        }
    };

    class Solution2
    {
        internal static bool ArrayStringAreEqual(string[] word1, string[] word2)
        {
            string str1 = "";
            foreach (string st in word1)
                str1 += st;

            string str2 = "";
            foreach (string st in word2)
                str2 += st;

            return str1 == str2;
        }
    };

    public class Cat
    {
        int size;


        public int Size
        {
            get
            {
                return size;
            }
            set 
            {
                this.size = value;
            }
        }

        public Cat(int size)
        {
            this.size = size;
        }
    }
}
