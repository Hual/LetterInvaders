using System;
using System.Collections.Generic;
using System.Text;

namespace LetterInvaders
{
    class Entity
    {
        private int m_posX;
        private int m_posY;
        private int m_deltaX;
        private int m_deltaY;
        private char m_char;
        private ConsoleColor m_colour;

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

        protected char Character
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

        protected ConsoleColor Colour
        {
            get
            {
                return m_colour;
            }
            set
            {
                m_colour = value;
            }
        }

        public Entity(int x, int y, char chr = ' ', ConsoleColor colour = ConsoleColor.White)
        {
            m_posX = x;
            m_posY = y;
            m_deltaX = -1;
            m_deltaY = -1;
            m_char = chr;
            m_colour = colour;

        }

        public void stepLeft()
        {
            --X;
        }

        public void stepRight()
        {
            ++X;
        }

        public void stepUp()
        {
            --Y;
        }

        public void stepDown()
        {
            ++Y;
        }

        public void draw(ConsoleCanvas canvas)
        {
            if (m_deltaX == -1)
            {
                canvas.drawLetter(m_posX, m_posY, m_char, m_colour);

                m_deltaX = m_posX;
                m_deltaY = m_posY;
            }
            else if (m_posX != m_deltaX || m_posY != m_deltaY)
            {
                canvas.clear(m_deltaX, m_deltaY);
                canvas.drawLetter(m_posX, m_posY, m_char, m_colour);

                m_deltaX = m_posX;
                m_deltaY = m_posY;
            }
        }

        public void remove(ConsoleCanvas canvas)
        {
            if (m_deltaX == -1)
                canvas.clear(m_posX, m_posY);
            else
                canvas.clear(m_deltaX, m_deltaY);
        }
    }
}
