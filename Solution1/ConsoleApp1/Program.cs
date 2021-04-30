using System;
using System.IO;
using System.Xml.Serialization;

namespace ConsoleApp1
{
    class Program
    {
        public static void Save(string title, Cat cat)
        {
            using (FileStream fs = new FileStream(title + ".xml", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                XmlSerializer xs = new XmlSerializer(typeof(Cat));
                xs.Serialize(fs, cat);
            }
        }

        public static Cat Load(string title)
        {
            Cat res = null;
            using (FileStream fs = new FileStream(title + ".xml", FileMode.Open, FileAccess.Read))
            {
                XmlSerializer xs = new XmlSerializer(typeof(Cat));
                res = xs.Deserialize(fs) as Cat;
            }

            return res;
        }


        static void Main(string[] args)
        {
            Cat cat = new Cat("black", 5);

            Save("cat", cat);

            Cat cat1 = Load("cat");

            Console.WriteLine(cat.Color + "/t" + cat.Weight);
            Console.WriteLine(cat1.Color + "/t" + cat1.Weight);

            cat1.Color = "white";

            Console.WriteLine(cat.Color + "/t" + cat1.Color);
        }

    }

    public class Cat
    {
        string color;
        double weight;

        public string Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
            }
        }
        public double Weight
        {
            get
            {
                return weight;
            }
            set
            {
                weight = value;
            }
        }

        public Cat ()
        {

        }

        public Cat(string color, double weight)
        {
            this.Color = color;
            this.Weight = weight;
        }
    }
}
