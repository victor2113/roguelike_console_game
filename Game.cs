using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace RogueFefu
{
    internal class Game
    {

        public void Begin()
        {
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.DarkYellow;

            Console.WriteLine("╔════════════════════════════════════════════════════╗");
            Console.WriteLine("║                                                    ║");
            Console.WriteLine("║          ██╗  ██╗███████╗██████╗  ██████╗          ║");
            Console.WriteLine("║          ██║  ██║██╔════╝██╔══██╗██╔═══██╗         ║");
            Console.WriteLine("║          ███████║█████╗  ██████╔╝██║   ██║         ║");
            Console.WriteLine("║          ██╔══██║██╔══╝  ██╔══██╗██║   ██║         ║");
            Console.WriteLine("║          ██║  ██║███████╗██║  ██║╚██████╔╝         ║");
            Console.WriteLine("║          ╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝ ╚═════╝          ║");
            Console.WriteLine("║                                                    ║");
            Console.WriteLine("║            Please, enter your player name          ║");
            Console.WriteLine("║                                                    ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");
            Console.Write("Name: ");

            string playerName = Console.ReadLine();
            Player player = new Player(playerName);

            string promt = $@"
                                                                                                                 
███████╗███████╗███████╗██╗   ██╗    ██████╗ ██╗   ██╗███╗   ██╗ ██████╗ ███████╗ ██████╗ ███╗   ██╗             
██╔════╝██╔════╝██╔════╝██║   ██║    ██╔══██╗██║   ██║████╗  ██║██╔════╝ ██╔════╝██╔═══██╗████╗  ██║             
█████╗  █████╗  █████╗  ██║   ██║    ██║  ██║██║   ██║██╔██╗ ██║██║  ███╗█████╗  ██║   ██║██╔██╗ ██║             
██╔══╝  ██╔══╝  ██╔══╝  ██║   ██║    ██║  ██║██║   ██║██║╚██╗██║██║   ██║██╔══╝  ██║   ██║██║╚██╗██║             
██║     ███████╗██║     ╚██████╔╝    ██████╔╝╚██████╔╝██║ ╚████║╚██████╔╝███████╗╚██████╔╝██║ ╚████║             
╚═╝     ╚══════╝╚═╝      ╚═════╝     ╚═════╝  ╚═════╝ ╚═╝  ╚═══╝ ╚═════╝ ╚══════╝ ╚═════╝ ╚═╝  ╚═══╝   
                                                                              RogueLike game project

Hello, {player.PlayerName}! Let's start the game!
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
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(level);

        }
        private void Generate()
        {
            for (int i = 0; i < 101; i++)
                LoadMapLevel();

        }

    }

}