using System;
using System.Collections.Generic;
using System.Text;

namespace LetterInvaders
{
    class Entity
    {
        private int m_posX;
        private int m_posY;
        private char m_char;

        public int X
        {
            get
            {
                return m_posX;
            }
            set
            {
                m_posX = value;
            }
        }

        public int Y
        {
            get
            {
                return m_posY;
            }
            set
            {
                m_posY = value;
            }
        }
        
        public char Character
        {
            get
            {
                return m_char;
            }
            set
            {
                m_char = value;
            }
        }

        public Entity(int x, int y, char chr)
        {
            m_posX = x;
            m_posY = y;
            m_char = chr;
        }

        public void stepLeft()
        {
            --m_posX;
        }

        public void stepRight()
        {
            ++m_posX;
        }

        public void stepUp()
        {
            --m_posY;
        }

        public void stepDown()
        {
            ++m_posY;
        }

        public void draw(ConsoleBuffer buffer)
        {
            buffer.drawLetter(m_posX, m_posY, m_char);
        }
    }
}
