using System;
using System.Collections.Generic;
using System.Text;

namespace LetterInvaders
{
    class Bullet : Entity
    {
        private ulong m_lastMoved;

        public ulong LastMoved
        {
            get
            {
                return m_lastMoved;
            }
        }

        public Bullet(int x, int y) : base(x, y, '|')
        {
            m_lastMoved = 0;
        }

        public void step(ulong tickCount)
        {
            if (tickCount - m_lastMoved > 1)
            {
                stepUp();
                m_lastMoved = tickCount;
            }
        }
    }
}
