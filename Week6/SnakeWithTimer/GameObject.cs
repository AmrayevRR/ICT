﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Example1
{
    public abstract class GameObject
    {
        public char sign;
        public ConsoleColor color;
        public List<Point> body;

        public GameObject()
        {

        }

        public GameObject(char sign, ConsoleColor color)
        {
            this.sign = sign;
            this.color = color;
            this.body = new List<Point>();
        }
        protected void Draw()
        {
            Console.ForegroundColor = color;
            for (int i = 0; i < body.Count; ++i)
            {
                Console.SetCursorPosition(body[i].X, body[i].Y);
                Console.Write(sign);
            }
        }

        protected void Clear()
        {
            for (int i = 0; i < body.Count; ++i)
            {
                Console.SetCursorPosition(body[i].X, body[i].Y);
                Console.Write(' ');
            }
        }

        public static void drawGameObject (GameObject g)
        {
            Console.ForegroundColor = g.color;
            for (int i = 0; i < g.body.Count; ++i)
            {
                Console.SetCursorPosition(g.body[i].X, g.body[i].Y);
                Console.Write(g.sign);
            }
        }
        public static void clearGameObject (GameObject g)
        {
            for (int i = 0; i < g.body.Count; ++i)
            {
                Console.SetCursorPosition(g.body[i].X, g.body[i].Y);
                Console.Write(' ');
            }
        }
    }
}