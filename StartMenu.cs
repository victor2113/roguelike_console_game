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

        public StartMenu(string[] options)
        {
            this.SelectedIndex = 0;
            this.options = options;
        }

        private void DisplayOptions()
        {
            if (options.Length > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                UserInterface.ConsoleWriteln("═════════════════", UserInterface.MapWidth, 2);
                UserInterface.ConsoleWriteln(" GAME MENU:", UserInterface.MapWidth, 3);
                UserInterface.ConsoleWriteln("═════════════════", UserInterface.MapWidth, 4);
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
                    UserInterface.ConsoleWriteln($"{prefix}{CurentOption}   ", UserInterface.MapWidth, 6 + i);
                }
            }
        }

        public int Run()
        {
            ConsoleKey keyPressed;
            do
            {
                DisplayOptions();

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.UpArrow)
                {
                    SelectedIndex--;
                    if(SelectedIndex == -1)
                    {
                        SelectedIndex = options.Length - 1;
                    }
                }else if(keyPressed == ConsoleKey.DownArrow)
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
