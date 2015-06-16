using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LetterInvaders
{
    sealed class HUD
    {
        private ConsoleCanvas m_canvas;

        public HUD(ConsoleCanvas canvas)
        {
            m_canvas = canvas;

            m_canvas.drawRect(0, 41, 80, 9, ConsoleColor.DarkMagenta, ConsoleColor.DarkMagenta);
            m_canvas.fillRect(1, 42, 78, 7, ConsoleColor.Gray);

            m_canvas.drawString(9, 44, "Health", ConsoleColor.Black, ConsoleColor.Gray);
            updateHealth(5);

            m_canvas.drawString(37, 44, "Score", ConsoleColor.Black, ConsoleColor.Gray);
            updateScore(0);

            m_canvas.drawString(65, 44, "Laser", ConsoleColor.Black, ConsoleColor.Gray);
            updateLaser(5);
        }

        public void updateHealth(byte amount)
        {
            m_canvas.drawLineHorz(2, 46, 4 * amount, ConsoleColor.Red, ConsoleColor.Red);
            m_canvas.drawLineHorz(2 + 4 * amount, 46, 20 - 4 * amount, ConsoleColor.DarkRed, ConsoleColor.DarkRed);
        }

        public void updateLaser(byte amount)
        {
            m_canvas.drawLineHorz(58, 46, 4 * amount, ConsoleColor.Green, ConsoleColor.Green);
            m_canvas.drawLineHorz(58 + 4 * amount, 46, 20 - 4 * amount, ConsoleColor.DarkGreen, ConsoleColor.DarkGreen);
        }

        public void updateScore(uint score)
        {
            m_canvas.drawString(36, 46, score.ToString().PadLeft(7, '0'), ConsoleColor.Black, ConsoleColor.Gray);
        }
    }
}
