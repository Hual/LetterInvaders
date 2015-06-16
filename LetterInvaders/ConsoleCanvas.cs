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

            Console.SetBufferSize(m_width + 1, m_height);
            Console.SetWindowSize(m_width, m_height);

            Console.CursorVisible = false;
            Console.OutputEncoding = Encoding.ASCII;
            Console.Out.NewLine = "\n";
        }

        public void drawLetter(int x, int y, char chr, ConsoleColor colour = ConsoleColor.White, ConsoleColor background = (ConsoleColor)16)
        {
            Console.ResetColor();
            Console.ForegroundColor = colour;

            if ((byte)background != 16)
                Console.BackgroundColor = background;

            Console.SetCursorPosition(x, y);
            Console.Write(chr);
            Console.Out.Flush();
        }

        public void drawString(int x, int y, string text, ConsoleColor colour = ConsoleColor.White, ConsoleColor background = (ConsoleColor)16)
        {
            Console.ResetColor();
            Console.ForegroundColor = colour;

            if ((byte)background != 16)
                Console.BackgroundColor = background;

            Console.SetCursorPosition(x, y);
            Console.Write(text);
            Console.Out.Flush();
        }

        public void drawLineHorz(int x, int y, int length, ConsoleColor colour = ConsoleColor.White, ConsoleColor background = (ConsoleColor)16)
        {
            drawString(x, y, String.Empty.PadRight(length, ' '), colour, background);
        }

        public void drawLineVert(int x, int y, int length, ConsoleColor colour = ConsoleColor.White, ConsoleColor background = (ConsoleColor)16)
        {
            for (int i = 0; i < length; ++i)
                drawLetter(x, y + i, ' ', colour, background);

        }

        public void drawRect(int x, int y, int width, int height, ConsoleColor colour = ConsoleColor.White, ConsoleColor background = (ConsoleColor)16)
        {
            drawLineHorz(x, y, width, colour, background);
            drawLineHorz(x, y + height - 1, width, colour, background);
            drawLineVert(x, y + 1, height - 2, colour, background);
            drawLineVert(x + width - 1, y + 1, height - 2, colour, background);
        }

        public void fillRect(int x, int y, int width, int height, ConsoleColor colour)
        {
            for (int i = 0; i < height; ++i)
                drawLineHorz(x, y + i, width, colour, colour);
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
                clearLineHorz(x, y + i, width);

        }

        public void reset()
        {
            Console.Clear();
        }
    }
}
