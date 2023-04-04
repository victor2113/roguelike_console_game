using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace RogueFefu
{
    internal class Game
    {
        private const int ARROWUP = 0;
        private const int ARROWDOWN = 1;
        private const int ARROWLEFT = 2;
        private const int ARROWRIGHT = 3;
        private const int ESCAPE = 4;



        private bool GameOver = false;
        private int ButtonPressed;

        public MapLevel CurrentMap { get; set; }
        public int CurrentLevel { get; set; }
        public Player CurrentPlayer { get; }
        public int CurrentTurn { get; set; }



        public Game()
        {
            string PlayerName = "Tolya";
            this.CurrentLevel = 1;
            this.CurrentMap = new MapLevel();
            this.CurrentPlayer = new Player(PlayerName);
            //this.CurrentPlayer.Location = CurrentMap.PlaceMapCharacter(Player.CHARACTER, true);
        }


        public Game(string PlayerName)
        {
            this.CurrentLevel = 1;
            this.CurrentMap = new MapLevel();
            this.CurrentPlayer = new Player(PlayerName);
            //this.CurrentPlayer.Location = CurrentMap.PlaceMapCharacter(Player.CHARACTER, true);
        }

        public void Begin()
        {
            Console.CursorVisible = false;
            string promt = $@"
                                                                                                                 
███████╗███████╗███████╗██╗   ██╗    ██████╗ ██╗   ██╗███╗   ██╗ ██████╗ ███████╗ ██████╗ ███╗   ██╗             
██╔════╝██╔════╝██╔════╝██║   ██║    ██╔══██╗██║   ██║████╗  ██║██╔════╝ ██╔════╝██╔═══██╗████╗  ██║             
█████╗  █████╗  █████╗  ██║   ██║    ██║  ██║██║   ██║██╔██╗ ██║██║  ███╗█████╗  ██║   ██║██╔██╗ ██║             
██╔══╝  ██╔══╝  ██╔══╝  ██║   ██║    ██║  ██║██║   ██║██║╚██╗██║██║   ██║██╔══╝  ██║   ██║██║╚██╗██║             
██║     ███████╗██║     ╚██████╔╝    ██████╔╝╚██████╔╝██║ ╚████║╚██████╔╝███████╗╚██████╔╝██║ ╚████║             
╚═╝     ╚══════╝╚═╝      ╚═════╝     ╚═════╝  ╚═════╝ ╚═╝  ╚═══╝ ╚═════╝ ╚══════╝ ╚═════╝ ╚═╝  ╚═══╝   
                                                                              RogueLike game project
Hello, {this.CurrentPlayer.PlayerName}! Let's start the game!
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
            string[] options = { "Play", "Menu", "Exit" };
            StartMenu startMenu = new StartMenu(options, "Map");
            int selectedIndex = startMenu.Run();
            switch (selectedIndex)
            {
                case 0:
                    LoadmapAndPlay();
                    break;
                case 1:
                    Begin();
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

        




        void LoadmapAndPlay()
        {
            Console.Clear();
            LoadMapLevel();
            do
            {
                int key = ReadKeyPressedByPlayer();
                switch (key)
                {
                    case ARROWUP:
                        
                        Console.WriteLine("U moved UP");
                        break;
                    case ARROWDOWN:
                        Console.WriteLine("U moved DOWN");
                        break;
                    case ARROWLEFT:
                        Console.WriteLine("U moved LEFT");
                        break;
                    case ARROWRIGHT:
                        Console.WriteLine("U moved RIGHT");
                        break;
                    case ESCAPE:
                        GameOver = true;
                        break;
                }
            } while (!GameOver);
    }

         



        private int ReadKeyPressedByPlayer()
        {
            ConsoleKey keyPressed;
           
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.UpArrow)
                {
                    ButtonPressed = 0;
                  
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    ButtonPressed = 1 ;
                }
                else if (keyPressed == ConsoleKey.LeftArrow)
                {
                    ButtonPressed = 2;
                }
                else if (keyPressed == ConsoleKey.RightArrow)
                {
                    ButtonPressed = 3;
                }
            else if (keyPressed == ConsoleKey.Escape)
            {
                ButtonPressed = 4;
            }
            return ButtonPressed;
        }

        public void MoveCharacter(Player player, MapLevel.Direction direct)
        {
            // Move character if possible.

            // List of characters a living character can move onto.
            List<char> charsAllowed =
                new List<char>(){MapLevel.ROOM_INT, MapLevel.STAIRWAY,
                MapLevel.ROOM_DOOR, MapLevel.HALLWAY};

            // Set surrounding characters
            Dictionary<MapLevel.Direction, MapSpace> surrounding =
                CurrentMap.SearchAdjacent(player.Location.X, player.Location.Y);

            // If the map character in the chosen direction is habitable 
            // and if there's no monster there,move the character there.
            
            if (charsAllowed.Contains(surrounding[direct].MapCharacter) &&
                surrounding[direct].DisplayCharacter == null)
                    player.Location = CurrentMap.MoveDisplayItem(player.Location, surrounding[direct]);

        }
    }

}