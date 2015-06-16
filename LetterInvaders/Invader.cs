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

            m_lastMoved = 0;
            Character = (char)seed.Next(0x23, 0x7A);
            Colour = (ConsoleColor)seed.Next(1, 15);
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
