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

        public const string GameLogo = @"
                                                                                                                 
███████╗███████╗███████╗██╗   ██╗    ██████╗ ██╗   ██╗███╗   ██╗ ██████╗ ███████╗ ██████╗ ███╗   ██╗             
██╔════╝██╔════╝██╔════╝██║   ██║    ██╔══██╗██║   ██║████╗  ██║██╔════╝ ██╔════╝██╔═══██╗████╗  ██║             
█████╗  █████╗  █████╗  ██║   ██║    ██║  ██║██║   ██║██╔██╗ ██║██║  ███╗█████╗  ██║   ██║██╔██╗ ██║             
██╔══╝  ██╔══╝  ██╔══╝  ██║   ██║    ██║  ██║██║   ██║██║╚██╗██║██║   ██║██╔══╝  ██║   ██║██║╚██╗██║             
██║     ███████╗██║     ╚██████╔╝    ██████╔╝╚██████╔╝██║ ╚████║╚██████╔╝███████╗╚██████╔╝██║ ╚████║             
╚═╝     ╚══════╝╚═╝      ╚═════╝     ╚═════╝  ╚═════╝ ╚═╝  ╚═══╝ ╚═════╝ ╚══════╝ ╚═════╝ ╚═╝  ╚═══╝   
                                                                              RogueLike game project
";

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
        }

        public void Begin()
        {
            string logo = $"{GameLogo}\n\n" + $"Hello, {this.CurrentPlayer.PlayerName}! Let's start the game!";

            Console.CursorVisible = false;
            string[] options = { "Start", "About", "Exit" };
            StartMenu startMenu = new StartMenu(options);
            UserInterface ui = new UserInterface(logo, "Use the arrow keys to choose options and press enter to select one", startMenu);
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


        private string LoadMapLevel()
        {
            string level = this.CurrentMap.MapText();
            Console.ForegroundColor = ConsoleColor.Gray;
            return level;
        }

        void LoadmapAndPlay()
        {
            UserInterface ui = new UserInterface(LoadMapLevel(), "Use arrows keys to walk", null!);
            do
            {
                int key = ReadKeyPressedByPlayer();
                switch (key)
                {
                    case ARROWUP:
                        MoveCharacter(CurrentPlayer, MapLevel.Direction.North);
                        ui.UpdateUi(LoadMapLevel(), "Use arrows keys to walk", ui.Menu);
                        break;
                    case ARROWDOWN:
                        MoveCharacter(CurrentPlayer, MapLevel.Direction.South);
                        ui.UpdateUi(LoadMapLevel(), "Use arrows keys to walk", ui.Menu);
                        break;
                    case ARROWLEFT:
                        MoveCharacter(CurrentPlayer, MapLevel.Direction.West);
                        ui.UpdateUi(LoadMapLevel(), "Use arrows keys to walk", ui.Menu);
                        break;
                    case ARROWRIGHT:
                        MoveCharacter(CurrentPlayer, MapLevel.Direction.East);
                        ui.UpdateUi(LoadMapLevel(), "Use arrows keys to walk", ui.Menu);
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
                //MapLevel.ROOM_DOOR, MapLevel.HALLWAY , MapLevel.ENEMY},
                MapLevel.ROOM_DOOR, MapLevel.HALLWAY , MapLevel.DEALER};
        List<char?> charsEvent = new List<char?>() { MapLevel.DEALER }; ;


            Dictionary<MapLevel.Direction, MapSpace> surrounding =
                CurrentMap.SearchAdjacent(player.Location.X, player.Location.Y);


            if (charsEvent.Contains(surrounding[direct].ItemCharacter))
            {
                // TODO: replace Console.Write with UserInterface call
                Console.WriteLine("fight!");
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