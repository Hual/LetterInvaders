using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace LetterInvaders
{
    class Program
    {
        static void Main(string[] args)
        {
            if (Console.LargestWindowHeight < 50)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("ERROR: Your system does not meet the minimum requirements");
                Console.ReadKey();
                Environment.Exit(1);
            }

            ConsoleCanvas canvas = new ConsoleCanvas(80, 50);
            HUD hud = new HUD(canvas);

            Game.start(hud);

            while (true)
            {
                Game.tick(canvas);

                Thread.Sleep(50);
            }
        }
    }
}
