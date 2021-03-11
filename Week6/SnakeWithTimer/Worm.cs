using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Timers;
using System.Xml.Serialization;

namespace Example1
{
    public class Worm : GameObject
    {
        public int Dx { get; set; }
        public int Dy { get; set; }

        public Worm() : base()
        {

        }

        public Worm(char sign, ConsoleColor color) : base(sign, color)
        {
            Point head = new Point { X = 20, Y = 20 };
            body.Add(head);
            Draw();
        }

        public void ChangeDirection(int dx, int dy)
        {
            Dx = dx;
            Dy = dy;
        }

        public void Move()
        {
            //Clear();
           
            for (int i = body.Count - 1; i > 0; --i)
            {
                body[i].X = body[i - 1].X;
                body[i].Y = body[i - 1].Y;
            }

            body[0].X += Dx;
            body[0].Y += Dy;

            //Draw();
        }

        public void Increase(Point point)
        {
            body.Add(new Point { X = point.X, Y = point.Y });
        }

        public List<Point> getBody ()
        {
            return body;
        }
        public ConsoleColor getColor ()
        {
            return color;
        }
        public char gateChar ()
        {
            return sign;
        }
        public void Save(string title)
        {
            using (FileStream fs = new FileStream(title + ".xml", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                XmlSerializer xs = new XmlSerializer(typeof(Worm));
                xs.Serialize(fs, this);
            }
        }

        public static Worm Load(string title)
        {
            Worm res = null;
            using (FileStream fs = new FileStream(title + ".xml", FileMode.Open, FileAccess.Read))
            {
                XmlSerializer xs = new XmlSerializer(typeof(Worm));
                res = xs.Deserialize(fs) as Worm;
            }
            return res;
        }
        
        public static bool IsLegalMove(Worm worm, Wall wall)
        {
            for (int i = 0; i < wall.body.Count; i++)
            {
                if ((worm.body[0].X + worm.Dx == wall.body[i].X && worm.body[0].Y == wall.body[i].Y) || (worm.body[0].X == wall.body[i].X && worm.body[0].Y + worm.Dy == wall.body[i].Y))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
