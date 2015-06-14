using System;
using System.Collections.Generic;
using System.Text;

namespace LetterInvaders
{
    class Invader : Entity
    {
        private ulong m_lastMoved;

        public ulong LastMoved
        {
            get
            {
                return m_lastMoved;
            }
        }

        public Invader(int x, int y) : base(x, y, (char)new Random().Next(0x23, 0x7A))
        {
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
