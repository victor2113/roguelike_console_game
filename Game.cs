using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;

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
        public Battle battlelvl{ get; set; }
        public Enemy CurrentEnemy { get; set; }
        public Game()
        {
            string PlayerName = "Tolya";
            this.CurrentLevel = 1;
            this.CurrentMap = new MapLevel();
            this.CurrentPlayer = new Player(PlayerName);
            this.CurrentPlayer.Location = CurrentMap.PlaceMapCharacter(Player.CHARACTER, true);
        }

        public Game(string PlayerName)
        {
            this.CurrentLevel = 1;
            this.CurrentMap = new MapLevel();
            this.CurrentPlayer = new Player(PlayerName);
            this.CurrentPlayer.Location = CurrentMap.PlaceMapCharacter(Player.CHARACTER, true);
            this.battlelvl = new Battle();
            this.CurrentEnemy = new Enemy();
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
                    LoadmapAndPlay();
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





        private void LoadMapLevel()
        {
            string level = this.CurrentMap.MapText();
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

                        MoveCharacter(CurrentPlayer, MapLevel.Direction.North);
                        Console.Clear();
                        LoadMapLevel();
                        break;
                    case ARROWDOWN:
                        MoveCharacter(CurrentPlayer, MapLevel.Direction.South);
                        Console.Clear();
                        LoadMapLevel();
                        break;
                    case ARROWLEFT:
                        MoveCharacter(CurrentPlayer, MapLevel.Direction.West);
                        Console.Clear();
                        LoadMapLevel();
                        break;
                    case ARROWRIGHT:
                        MoveCharacter(CurrentPlayer, MapLevel.Direction.East);
                        Console.Clear();
                        LoadMapLevel();
                        break;
                    case ESCAPE:
                        ExitGame();
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
                ButtonPressed = 1;
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

            List<char> charsAllowed =
                new List<char>(){MapLevel.ROOM_INT, MapLevel.STAIRWAY,
                MapLevel.ROOM_DOOR, MapLevel.HALLWAY , MapLevel.ENEMY};
            List<char?> charsEvent = new List<char?>() { MapLevel.ENEMY }; ;


            Dictionary<MapLevel.Direction, MapSpace> surrounding =
                CurrentMap.SearchAdjacent(player.Location.X, player.Location.Y);

            if (charsEvent.Contains(surrounding[direct].ItemCharacter))
            {
                Console.WriteLine("fight!");
                battlelvl.Begin(CurrentPlayer, CurrentEnemy);
                player.Location = CurrentMap.MoveDisplayItem(player.Location, surrounding[direct]);
            }


            if (charsAllowed.Contains(surrounding[direct].MapCharacter) &&
                surrounding[direct].DisplayCharacter == null)
            {
                player.Location = CurrentMap.MoveDisplayItem(player.Location, surrounding[direct]);
            }


        }
    }

}