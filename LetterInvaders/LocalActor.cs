using System;
using System.Collections.Generic;
using System.Text;

namespace LetterInvaders
{
    class LocalActor : Entity
    {
        private ulong m_lastShot;

        public ulong LastShot
        {
            get
            {
                return m_lastShot;
            }
            set
            {
                m_lastShot = value;
            }
        }

        public LocalActor(int x, int y) : base(x, y, '!')
        {
            m_lastShot = 0;
        }
    }
}
