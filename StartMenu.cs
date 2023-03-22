using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueFefu
{
    internal class StartMenu
    {
        private int SelectedIndex;
        private string[] options;
        private string promt;

        public StartMenu(string[] options, string promt)
        {
            this.SelectedIndex = 0;
            this.options = options;
            this.promt = promt;
        }

        private void DisplayOptions()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(promt);
            string prefix;
            for (int i = 0; i < options.Length; i++)
            {
                string CurentOption = options[i];
                if (i == SelectedIndex)
                {
                    prefix = "-->";
                    Console.ForegroundColor = ConsoleColor.DarkBlue;

                }
                else
                {
                    prefix = " ";
                    Console.ForegroundColor = ConsoleColor.White;

                }

                Console.WriteLine($"{prefix}{CurentOption} ");

            }
        }
        public int Run()
        {
            ConsoleKey keyPressed;
            do
            {
                Console.Clear();
                DisplayOptions();

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.UpArrow)
                {
                    SelectedIndex--;
                    if (SelectedIndex == -1)
                    {
                        SelectedIndex = options.Length - 1;
                    }
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    SelectedIndex++;
                    if (SelectedIndex == options.Length)
                    {
                        SelectedIndex = 0;
                    }
                }

            } while (keyPressed != ConsoleKey.Enter);
            return SelectedIndex;
        }
    }
}