using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace RogueFefu
{
    internal class Game
    {

        public string GameLogo = @"
  ______    __             _                                    
 |  ____|  / _|           | |                                   
 | |__ ___| |_ _   _    __| |_   _ _ __   __ _  ___  ___  _ __  
 |  __/ _ \  _| | | |  / _` | | | | '_ \ / _` |/ _ \/ _ \| '_ \ 
 | | |  __/ | | |_| | | (_| | |_| | | | | (_| |  __/ (_) | | | |
 |_|  \___|_|  \__,_|  \__,_|\__,_|_| |_|\__, |\___|\___/|_| |_|
                                          __/ |                 
                                         |___/ RogueLike game project


";


        public void Begin()
        {
            Console.CursorVisible = false;



            string[] options = { "Start", "About", "Exit" };
            StartMenu startMenu = new StartMenu(options);
            UserInterface ui = new UserInterface(GameLogo, "Use the arrow keys to choose options and press enter to select one", startMenu);
            int selectedIndex = startMenu.Run();

            


            switch (selectedIndex)
            {
                case 0:
                    RunTheGame();
                    break;
                case 1:
                    AboutGameText();
                    break;
                case 2:
                    ExitGame();
                    break;


            }

            Console.ReadKey(true);
        }

        private void ExitGame()
        {
            UserInterface ui = new UserInterface(GameLogo, "Press any key to exit....", null!);
            Console.ReadKey(true);
            Environment.Exit(0);
        }
        private void AboutGameText()
        {
            UserInterface ui = new UserInterface($"{GameLogo}\n\nBla bla bla", "Press any key to return", null!);
            Console.ReadKey(true);
            Begin();
        }

        private void RunTheGame()
        {
            string[] startOptions = { "Next", "Generate", "Battle", "Exit" };
            StartMenu startMenu = new StartMenu(startOptions);
            UserInterface ui = new UserInterface("Map", "", startMenu);
            int selectedIndex = startMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    ui.UpdateUi(LoadMapLevel(), " Use arrows keys to walk", ui.Menu);
                    Console.ReadKey(true);
                    RunTheGame();


                    break;
                case 1:
                    ui.UpdateUi(Generate(), " Use arrows keys to walk", ui.Menu);
                    Console.ReadKey(true);
                    RunTheGame();
                    break;
                case 2:
                    Battle b = new Battle();
                    b.Begin();
                    break;
                case 3:
                    ExitGame();
                    break;


            }

            Console.ReadKey(true);
        }
        
        private string LoadMapLevel()
        {
            MapLevel newLevel = new MapLevel();
            string level = newLevel.MapText();
            Console.ForegroundColor = ConsoleColor.White;
            return level;
        }
        
        private string Generate()
        {
            string map = string.Empty;
            for (int i = 0; i < 101; i++)
                map = LoadMapLevel();
            return map;
        }
    }
}
