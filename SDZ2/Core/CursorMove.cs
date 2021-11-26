using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDZ2.Core
{
    public class CursorMove
    {


        private readonly ConsoleColor _color = ConsoleColor.Green;
        public CursorMove(){}
        public CursorMove(ConsoleColor color)=>_color=color;

        enum CusorDir
        {
            Up, Down
        }

        public int run(int cursorPos = 0,int max = 1)
        {
            int baseMax = max;
            int basePos = cursorPos;
            max = max + cursorPos-1;
            int currentCursorPos = cursorPos;
            ConsoleKeyInfo key;
            if (baseMax == 0)
            {
                while (true)
                {
                    key = Console.ReadKey();
                    if (key.Key == ConsoleKey.Backspace)
                    {
                        return -1;
                    }
                    Console.SetCursorPosition(2, currentCursorPos);
                }
            }
            showCursor(currentCursorPos);
            while (true)
            {
                key = Console.ReadKey();
                if (key.Key == ConsoleKey.DownArrow && currentCursorPos < max)
                {
                    CursorMove(CusorDir.Down, ref currentCursorPos);
                }else
                if (key.Key == ConsoleKey.UpArrow && currentCursorPos > basePos)
                {
                    CursorMove(CusorDir.Up, ref currentCursorPos);
                }else
                if (key.Key == ConsoleKey.Enter)
                {
                    return currentCursorPos - basePos;
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    return -1;
                }

                Console.SetCursorPosition(2, currentCursorPos);
            }

            void showCursor(int CursorPos)
            {
                Console.SetCursorPosition(0, CursorPos);
                Console.Write("  ");
                Console.SetCursorPosition(0, CursorPos);
                Console.ForegroundColor = _color;
                Console.Write("->");
                Console.ResetColor();
            }

            void CursorMove(CusorDir dir, ref int CursorPos)
            {
                Console.SetCursorPosition(0, CursorPos);
                Console.Write("  ");
                if (dir == CusorDir.Up)
                    CursorPos -= 1;
                else
                    CursorPos += 1;
                Console.SetCursorPosition(0, CursorPos);
                Console.ForegroundColor = _color;
                Console.Write("->");
                Console.ResetColor();
            }
        }
    }
}
