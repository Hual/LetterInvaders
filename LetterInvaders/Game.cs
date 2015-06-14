using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace LetterInvaders
{
    sealed class Game
    {
        public static LocalActor protagonist;
        public static List<Bullet> bullets;
        public static List<Invader> invaders;
        private static ulong tickCount;
        private static ulong lastSpawnedInvader;

        private static Mutex protagonistMutex;
        private static Mutex bulletsMutex;

        public static ulong TickCount
        {
            get
            {
                return tickCount;
            }
        }

        public static void start()
        {
            tickCount = 0;
            lastSpawnedInvader = 0;
            protagonist = new LocalActor(0, 39);
            bullets = new List<Bullet>();
            invaders = new List<Invader>();
            protagonistMutex = new Mutex(false);
            bulletsMutex = new Mutex(false);
            new Thread(KeyPressWorker).Start();


        }

        public static bool tick(ConsoleBuffer buffer)
        {
            int i = 0;

            if(TickCount - lastSpawnedInvader > 64)
            {
                invaders.Add(new Invader(new Random().Next(80), -1));
                lastSpawnedInvader = TickCount;
            }

            if (bulletsMutex.WaitOne())
            {
                while (i < bullets.Count)
                {
                    Bullet bullet = bullets[i];

                    bullet.step(TickCount);

                    if (bullet.Y < 0)
                    {
                        bullets.Remove(bullet);
                        continue;
                    }

                    if (checkCollision(bullet))
                        continue;

                    bullet.draw(buffer);

                    ++i;
                }

                bulletsMutex.ReleaseMutex();
            }

            i = 0;

            while(i < invaders.Count)
            {
                Invader invader = invaders[i];

                invader.step(TickCount);

                if (invader.Y >= protagonist.Y)
                {
                    invaders.Remove(invader);
                    continue;
                }

                invader.draw(buffer);

                ++i;
            }

            if (protagonistMutex.WaitOne())
            {
                protagonist.draw(buffer);
                protagonistMutex.ReleaseMutex();
            }

            ++tickCount;
            return true;
        }

        private static bool checkCollision(Bullet bullet)
        {
            foreach (Invader invader in invaders)
            {
                if (bullet.Y == invader.Y && bullet.X == invader.X)
                {
                    bullets.Remove(bullet);
                    invaders.Remove(invader);
                    return true;
                }
            }

            return false;
        }

        private static void KeyPressWorker()
        {
            while (true)
            {
                ConsoleKey key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.RightArrow)
                {
                    if (protagonistMutex.WaitOne())
                    {
                        protagonist.stepRight();
                        if (protagonist.X >= 80)
                            protagonist.X = 0;

                        protagonistMutex.ReleaseMutex();
                    }

                }
                else if (key == ConsoleKey.LeftArrow)
                {
                    if (protagonistMutex.WaitOne())
                    {
                        protagonist.stepLeft();
                        if (protagonist.X < 0)
                            protagonist.X = 79;

                        protagonistMutex.ReleaseMutex();
                    }
                }
                else if (key == ConsoleKey.Spacebar && TickCount - protagonist.LastShot > 32)
                {
                    if (bulletsMutex.WaitOne())
                    {
                        bullets.Add(new Bullet(protagonist.X, protagonist.Y - 1));
                        bulletsMutex.ReleaseMutex();
                    }

                    if (protagonistMutex.WaitOne())
                    {
                        protagonist.LastShot = TickCount;
                        protagonistMutex.ReleaseMutex();
                    }
                }
            }
        }
    }
}
