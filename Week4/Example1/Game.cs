using System;
using System.Collections.Generic;
using System.Text;

namespace Example1
{
    class Game
    {
        public static int Width { get { return 40; } }
        public static int Height { get { return 40; } }


        static int level = 1;
        static string pathLevel = $@"Levels/Level{level}.txt";

        Worm w = new Worm('@', ConsoleColor.Green);

        Food f = new Food('$', ConsoleColor.Yellow);
        Wall wall = new Wall('#', ConsoleColor.DarkYellow, pathLevel);

        public bool IsRunning { get; set; }
        public Game ()
        {
            IsRunning = true;
            Console.CursorVisible = false;
            Console.SetWindowSize(Width, Height);
            Console.SetBufferSize(Width, Height);
        }

        bool CheckCollisionFoodWithWorm ()
        {
            return (w.body[0].X == f.body[0].X && w.body[0].Y == f.body[0].Y);
        }
        bool CheckCollisionFoodWithWall ()
        {
            for (int i = 0; i < wall.body.Count; i++)
            {
                if (wall.body[i].X == f.body[0].X && wall.body[i].Y == f.body[0].Y)
                {
                    return true;
                }
            }
            return false;
        }
        bool CheckFoodAppearOnWorm ()
        {
            for (int i = 0; i<w.body.Count; i++)
            {
                if (w.body[i].X == f.body[0].X && w.body[i].Y == f.body[0].Y)
                {
                    return true;
                }
            }
            return false;
        }

        public void KeyPressed (ConsoleKeyInfo pressedKey)
        {
            switch (pressedKey.Key)
            {
                case ConsoleKey.UpArrow:
                    w.Move(0, -1);
                    break;
                case ConsoleKey.DownArrow:
                    w.Move(0, 1);
                    break;
                case ConsoleKey.LeftArrow:
                    w.Move(-1, 0);
                    break;
                case ConsoleKey.RightArrow:
                    w.Move(1, 0);
                    break;
                case ConsoleKey.Escape:
                    IsRunning = false;
                    break;
            }
            if (CheckCollisionFoodWithWorm())
            {
                w.Increase(w.body[0]);
                f.Generate();
            }
            while (CheckCollisionFoodWithWall())
            {
                wall.drawWall(f.body[0].X, f.body[0].Y);
                f.Generate();
            }
            while (CheckFoodAppearOnWorm())
            {
                f.Generate();
            }

            if (w.body.Count > 3)
            {
                Console.Clear();
                w = new Worm('@', ConsoleColor.Green);

                f = new Food('$', ConsoleColor.Yellow);

                level++;
                pathLevel = $@"Levels/Level{level}.txt";
                wall = new Wall('#', ConsoleColor.DarkYellow, pathLevel);
            }
        }
    }
}
