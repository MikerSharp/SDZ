using System;

namespace SDZ2.Core
{
    public static class Display
    {
        public static int ShowWithSelect(string text, string[] items)
        {
            int lines = text.Split('\n', StringSplitOptions.RemoveEmptyEntries).Length;

            Console.Clear();
            Console.WriteLine(text);
            
            if (items == null || items.Length == 0)
            {
                ShowError($"Нет элементов",false);
            }
            else
            {
                foreach (var item in items)
                {
                    Console.WriteLine(item.WithSpace());
                }
            }
            CursorMove cm = new CursorMove();
            return cm.run(lines, items.Length);
        }

        public static int ShowWithSelect(string text, (string t1, string t2)[] items)
        {
            string[] tempArr = new string[items.Length];
            for (int i = 0; i < items.Length; i++)
            {
                tempArr[i] = $"{items[i].t1}:{items[i].t2}";
            }
            return ShowWithSelect(text, tempArr);
        }

        public static void ShowText(string text = "", bool consoleClear = false, bool wait = false)
        {
            if (consoleClear) Console.Clear();
            if (!string.IsNullOrEmpty(text)) Console.WriteLine(text);
            if (wait)
            {
                Console.WriteLine("Нажмите любую клавишу для продолжения");
                Console.ReadKey();
            }
        }
        public static void ShowError(string text,bool wait = true)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(text);
            if (wait)
            {
                Console.WriteLine("Нажмите любую клавишу для продолжения");
                Console.ReadKey();
            }
            Console.ResetColor();
        }
    }
}
