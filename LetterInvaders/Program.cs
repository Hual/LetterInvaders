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
            ConsoleBuffer buffer = new ConsoleBuffer(80, 40);

            buffer.prepare();
            Game.start();

            new Thread(KeyPressWorker).Start();

            while (true)
            {
                buffer.reset();

                if(Game.tick(buffer))
                    buffer.write();

                Thread.Sleep(50);
            }
        }

        static void KeyPressWorker()
        {
            while (true)
            {
                Game.handleKeyPress(Console.ReadKey(true).Key);
            }
        }
    }
}
