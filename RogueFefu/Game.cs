using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueFefu
{
    internal class Game
    {
        public void Begin()
        {

            string promt = @"
  ______    __             _                                    
 |  ____|  / _|           | |                                   
 | |__ ___| |_ _   _    __| |_   _ _ __   __ _  ___  ___  _ __  
 |  __/ _ \  _| | | |  / _` | | | | '_ \ / _` |/ _ \/ _ \| '_ \ 
 | | |  __/ | | |_| | | (_| | |_| | | | | (_| |  __/ (_) | | | |
 |_|  \___|_|  \__,_|  \__,_|\__,_|_| |_|\__, |\___|\___/|_| |_|
                                          __/ |                 
                                         |___/ RogueLike game project


Use the arrow keys to choose options and press enter to select one";



            string[] options = { "Start", "About", "Exit" };
            StartMenu startMenu = new StartMenu(options , promt);
            int selectedIndex = startMenu.Run();



            switch (selectedIndex)
            {
                case 0:
                    RunTheGame();
                    break;
                case 1:
                    AboutText();
                    break;
                case 2:
                    ExitGame();
                    break;


            }
            
            Console.ReadKey(true);

        }
        private void ExitGame()
        {
            Console.WriteLine("\nPress any key to exit....");
            Console.ReadKey(true);
            Environment.Exit(0);
        }
        private void AboutText()
        {
            Console.Clear();
            Console.WriteLine("bla bla");
            Console.ReadKey(true);
            Begin();
        }
        private void RunTheGame()
        {
            Console.Clear();
            Console.WriteLine("Game Started");
            ExitGame();
        }
    }

}
