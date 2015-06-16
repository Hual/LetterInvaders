using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LetterInvaders
{
    sealed class ConsoleCanvas
    {
        private int m_width;
        private int m_height;

        public ConsoleCanvas(int width, int height)
        {
            m_width = width;
            m_height = height;
        }

        public void prepare()
        {
            Console.SetBufferSize(m_width, m_height);
            Console.SetWindowSize(m_width, m_height);
            Console.CursorVisible = false;
            Console.OutputEncoding = Encoding.ASCII;
            Console.Out.NewLine = "\n";
        }

        public void drawLetter(int x, int y, char chr)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(chr);
            Console.Out.Flush();
        }

        public void drawString(int x, int y, string text)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(text);
            Console.Out.Flush();
        }

        public void drawLineHorz(int x, int y, int length)
        {
            drawString(x, y, String.Empty.PadRight(length, '-'));
        }

        public void drawLineVert(int x, int y, int length)
        {
            for (int i = 0; i < length; ++i)
                drawLetter(x, y + i, '|');

        }

        public void drawRect(int x, int y, int width, int height)
        {
            drawLineHorz(x, y, width);
            drawLineHorz(x, y + height - 1, width);
            drawLineVert(x, y + 1, height - 2);
            drawLineVert(x + width - 1, y + 1, height - 2);
        }

        public void clear(int x, int y)
        {
            drawLetter(x, y, ' ');
        }

        public void clearLineHorz(int x, int y, int length)
        {
            drawString(x, y, String.Empty.PadRight(length, ' '));
        }

        public void clearRect(int x, int y, int width, int height)
        {
            for (int i = 0; i < height; ++i)
            {
                clearLineHorz(x, y + i, width);
            }
        }

        public void reset()
        {
            Console.Clear();
        }
    }
}
