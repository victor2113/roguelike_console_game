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
        public MapLevel CurrentMap { get; set; }
        public int CurrentLevel { get; }
        public Player CurentPlayer { get; }
        public int CurrentTurn { get; }



        public void Begin()
        {
            Console.CursorVisible = false;
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
            StartMenu startMenu = new StartMenu(options, promt);
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
            Console.Clear();
            Console.WriteLine("\nPress any key to exit....");
            Console.ReadKey(true);
            Environment.Exit(0);
        }
        private void AboutGameText()
        {
            Console.Clear();
            Console.WriteLine("bla bla");
            Console.ReadKey(true);
            Begin();
        }
        private void RunTheGame()
        {
            string[] options = { "Next", "Generate", "Exit" };
            StartMenu startMenu = new StartMenu(options, "Map");
            int selectedIndex = startMenu.Run();
            switch (selectedIndex)
            {
                case 0:
                    Console.Clear();
                    LoadMapLevel();
                    Console.ReadKey(true);
                    RunTheGame();


                    break;
                case 1:
                    Generate();
                    Console.Clear();
                    LoadMapLevel();
                    Console.ReadKey(true);
                    RunTheGame();
                    break;
                case 2:
                    ExitGame();
                    break;


            }

            Console.ReadKey(true);
        }
        private void LoadMapLevel()
        {
            MapLevel newLevel = new MapLevel();
            string level = newLevel.MapText();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(level);

        }
        private void Generate()
        {
            for (int i = 0; i < 101; i++)
                LoadMapLevel();

        }


    }

}