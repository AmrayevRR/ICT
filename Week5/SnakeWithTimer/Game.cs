using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace Example1
{
    class Game
    {
        Timer wormTimer = new Timer(100);
        Timer gameTimer = new Timer(1000);

        public static int Width { get { return 40; } }
        public static int Height { get { return 40; } }


        static int level = 1;
        static string pathLevel = $@"Levels/Level{level}.txt";

        Worm w = new Worm('@', ConsoleColor.Green);

        Food f = new Food('$', ConsoleColor.Yellow);
        Wall wall = new Wall('#', ConsoleColor.DarkYellow, pathLevel);

        public bool IsRunning { get; set; }
        public Game()
        {
            gameTimer.Elapsed += GameTimer_Elapsed;
            gameTimer.Start();
            wormTimer.Elapsed += Move2;
            wormTimer.Start();
            IsRunning = true;
            Console.CursorVisible = false;
            Console.SetWindowSize(Width, Height);
            Console.SetBufferSize(Width, Height);
            GameObject.drawGameObject(f);
            GameObject.drawGameObject(wall);
        }

        bool CheckCollisionFoodWithWorm()
        {
            return w.body[0].X == f.body[0].X && w.body[0].Y == f.body[0].Y;
        }
        bool CheckCollisionFoodWithWall()
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
        bool CheckFoodAppearOnWorm()
        {
            for (int i = 0; i < w.body.Count; i++)
            {
                if (w.body[i].X == f.body[0].X && w.body[i].Y == f.body[0].Y)
                {
                    return true;
                }
            }
            return false;
        }

        void Move2 (object sender, ElapsedEventArgs e)
        {
            bool foodWasGenerated = false;

            GameObject.clearGameObject(w);

            w.Move();

            GameObject.drawGameObject(w);

            if (CheckCollisionFoodWithWorm())
            {
                w.Increase(w.body[0]);
                f.Generate();
                foodWasGenerated = true;
            }
            while (CheckCollisionFoodWithWall())
            {
                wall.drawWall(f.body[0].X, f.body[0].Y);
                f.Generate();
                foodWasGenerated = true;
            }
            while (CheckFoodAppearOnWorm())
            {
                f.Generate();
                foodWasGenerated = true;
            }

            if (foodWasGenerated)
                GameObject.drawGameObject(f);

            if (w.body.Count > 5)
            {
                Console.Clear();
                w = new Worm('@', ConsoleColor.Green);

                f = new Food('$', ConsoleColor.Yellow);
                GameObject.drawGameObject(f);

                level++;
                pathLevel = $@"Levels/Level{level}.txt";
                wall = new Wall('#', ConsoleColor.DarkYellow, pathLevel);
                GameObject.drawGameObject(wall);
            }
        }

        private void GameTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Console.Title = DateTime.Now.ToLongTimeString();
        }

        public void KeyPressed(ConsoleKeyInfo pressedKey)
        {
            switch (pressedKey.Key)
            {
                case ConsoleKey.UpArrow:
                    w.ChangeDirection(0, -1);
                    break;
                case ConsoleKey.DownArrow:
                    w.ChangeDirection(0, 1);
                    break;
                case ConsoleKey.LeftArrow:
                    w.ChangeDirection(-1, 0);
                    break;
                case ConsoleKey.RightArrow:
                    w.ChangeDirection(1, 0);
                    break;
                case ConsoleKey.Escape:
                    IsRunning = false;
                    wormTimer.Stop();
                    break;
                case ConsoleKey.Spacebar: 
                    if (wormTimer.Enabled)
                    {
                        wormTimer.Stop();
                    } 
                    else
                    {
                        wormTimer.Start();
                    }
                    break;
            }
        }
    }
}
