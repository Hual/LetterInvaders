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

        private static sbyte health;
        private static uint score;

        private static HUD hud;

        public static ulong TickCount
        {
            get
            {
                return tickCount;
            }
        }

        public static void start(HUD hud)
        {
            Game.hud = hud;

            tickCount = 35;
            lastSpawnedInvader = 35;
            protagonist = new LocalActor(1, 39);
            bullets = new List<Bullet>();
            invaders = new List<Invader>();
            protagonistMutex = new Mutex(false);
            bulletsMutex = new Mutex(false);
            health = 5;

            new Thread(KeyPressWorker).Start();
        }

        public static bool tick(ConsoleCanvas canvas)
        {
            int i = 0;

            if (TickCount - lastSpawnedInvader > 48)
            {
                invaders.Add(new Invader(new Random().Next(1, 79), 0));
                lastSpawnedInvader = TickCount;
            }

            ulong lastShot = TickCount - protagonist.LastShot;

            if (lastShot <= 35 && lastShot % 7 == 0)
            {
                hud.updateLaserBar((byte)(lastShot / 7));
            }

            if (bulletsMutex.WaitOne())
            {
                while (i < bullets.Count)
                {
                    Bullet bullet = bullets[i];

                    if (bullet.Y < 0)
                    {
                        bullet.remove(canvas);
                        bullets.Remove(bullet);
                        continue;
                    }

                    if (checkCollision(canvas, bullet))
                    {
                        setScore(getScore() + 1);
                        continue;
                    }

                    bullet.draw(canvas);

                    bullet.step(TickCount);

                    ++i;
                }

                bulletsMutex.ReleaseMutex();
            }

            i = 0;

            while (i < invaders.Count)
            {
                Invader invader = invaders[i];

                invader.step(TickCount);

                if (invader.Y >= protagonist.Y)
                {
                    invader.remove(canvas);
                    invaders.Remove(invader);

                    setHealth(getHealth() - 1);
                    continue;
                }

                invader.draw(canvas);

                ++i;
            }

            if (protagonistMutex.WaitOne())
            {
                protagonist.draw(canvas);
                protagonistMutex.ReleaseMutex();
            }

            ++tickCount;
            return true;
        }

        private static bool checkCollision(ConsoleCanvas canvas, Bullet bullet)
        {
            foreach (Invader invader in invaders)
            {
                if (bullet.Y == invader.Y && bullet.X == invader.X)
                {
                    bullet.remove(canvas);
                    invader.remove(canvas);

                    bullets.Remove(bullet);
                    invaders.Remove(invader);
                    return true;
                }
            }

            return false;
        }

        private static void setHealth(int health)
        {
            Game.health = (sbyte)health;
            hud.updateHealthBar((byte)health);
        }

        private static sbyte getHealth()
        {
            return health;
        }

        private static void setScore(uint score)
        {
            Game.score = score;
            hud.updateScore(score);
        }

        private static uint getScore()
        {
            return score;
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

                        if (protagonist.X >= 79)
                            protagonist.X = 1;

                        protagonistMutex.ReleaseMutex();
                    }

                }
                else if (key == ConsoleKey.LeftArrow)
                {
                    if (protagonistMutex.WaitOne())
                    {
                        protagonist.stepLeft();

                        if (protagonist.X <= 0)
                            protagonist.X = 78;

                        protagonistMutex.ReleaseMutex();
                    }
                }
                else if (key == ConsoleKey.Spacebar && TickCount - protagonist.LastShot > 35)
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

                    hud.updateLaserBar(0);
                }
            }
        }
    }
}
