using System;
using System.Collections.Generic;
using System.Text;

namespace LetterInvaders
{
    class Invader : Entity
    {
        private ulong m_lastMoved;
        private Random seed;

        public ulong LastMoved
        {
            get
            {
                return m_lastMoved;
            }
        }

        public Invader(int x, int y) : base(x, y)
        {
            seed = new Random();

            int multiplier = seed.Next(2);
            Character = (char)seed.Next(0x41 + 0x20 * multiplier, 0x5A + 0x20 * multiplier);
            Colour = (ConsoleColor)seed.Next(1, 15);

            m_lastMoved = 0;
        }

        public void step(ulong tickCount)
        {
            if (tickCount - m_lastMoved > 4)
            {
                stepDown();
                m_lastMoved = tickCount;
            }
        }
    }
}
