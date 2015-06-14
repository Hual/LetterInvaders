using System;
using System.Collections.Generic;
using System.Text;

namespace LetterInvaders
{
    sealed class ConsoleBuffer
    {
        private char[] m_buffer;
        private int m_width;
        private int m_height;

        public ConsoleBuffer(int width, int height)
        {
            m_buffer = new char[(width + 2) * height];

            m_width = width;
            m_height = height;

            for (int i = 0; i < height; ++i)
            {
                m_buffer[width + i * height] = '\r';
                m_buffer[(width + 1) + i * height] = '\n';
            }
        }

        public void prepare()
        {
            Console.SetBufferSize(m_width, m_height);
            Console.SetWindowSize(m_width, m_height);
            Console.CursorVisible = false;
        }

        public void drawLetter(int x, int y, char chr)
        {
            m_buffer[x + m_width * y] = chr;
        }

        public void drawString(int x, int y, string text)
        {
            for (int i = 0; i < text.Length; ++i)
                drawLetter(x + i, y, text[i]);

        }

        public void drawLineHorz(int x, int y, int length)
        {
            for (int i = 0; i < length; ++i)
                drawLetter(x + i, y, '-');

        }

        public void drawLineVert(int x, int y, int length)
        {
            for (int i = 0; i < length; ++i)
                drawLetter(x, y + i, '|');

        }

        public void drawRect(int x, int y, int width, int height)
        {
            drawLineHorz(x, y, width);
            drawLineHorz(x, y + height + 1, width);
            drawLineVert(x, y + 1, height);
            drawLineVert(x + width - 1, y + 1, height);
        }

        public void reset()
        {
            Console.Clear();
            fill(' ');
        }

        public void fill(char chr)
        {

            for (int i = 0; i < m_width; ++i)
                for (int j = 0; j < m_height; ++j)
                    drawLetter(i, j, chr);

        }

        public void write()
        {
            Console.Write(m_buffer);
        }
    }
}
