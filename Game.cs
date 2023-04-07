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
        private const int MAX_LEVEL = 26;


        private bool GameOver = false;
        private int ButtonPressed;

        public MapLevel CurrentMap { get; set; }
        public int CurrentLevel { get; set; }
        public Player CurrentPlayer { get; }
        public int CurrentTurn { get; set; }
        public Battle battlelvl { get; set; }
        public Enemy CurrentEnemy { get; set; }

        private static Random rand = new Random();

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
            this.battlelvl = new Battle();
            this.CurrentEnemy = new Enemy();
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
            UserInterface ui = new UserInterface(LoadMapLevel(), "Use arrows keys to walk, ESC to exit game.", null!);
            do
            {
                int key = ReadKeyPressedByPlayer();
                switch (key)
                {
                    case ARROWUP:
                        MoveCharacter(CurrentPlayer, MapLevel.Direction.North);
                        ui.UpdateUi(LoadMapLevel(), "Use arrows keys to walk, ESC to exit game.", ui.Menu);
                        break;
                    case ARROWDOWN:
                        MoveCharacter(CurrentPlayer, MapLevel.Direction.South);
                        ui.UpdateUi(LoadMapLevel(), "Use arrows keys to walk, ESC to exit game.", ui.Menu);
                        break;
                    case ARROWLEFT:
                        MoveCharacter(CurrentPlayer, MapLevel.Direction.West);
                        ui.UpdateUi(LoadMapLevel(), "Use arrows keys to walk, ESC to exit game.", ui.Menu);
                        break;
                    case ARROWRIGHT:
                        MoveCharacter(CurrentPlayer, MapLevel.Direction.East);
                        ui.UpdateUi(LoadMapLevel(), "Use arrows keys to walk, ESC to exit game.", ui.Menu);
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

        private void ChangeLevel(int Change)
        {
            bool allowPass = false;

            if (Change < 0)
            {
                allowPass = CurrentPlayer.HasAmulet && CurrentLevel > 1;
                //сообщение allowPass ? "" : "You cannot go that way.";
            }

            else if (Change > 0)
            {
                allowPass = CurrentLevel < MAX_LEVEL;
                //сообщение allowPass ? "" : "You have reached the bottom level.You must go the other way.";            
            }

            if (allowPass)
            {
                CurrentMap = new MapLevel();
                CurrentLevel += Change;
                CurrentPlayer.Location = CurrentMap.PlaceMapCharacter(Player.CHARACTER, true);

                if (CurrentLevel == MAX_LEVEL && !CurrentPlayer.HasAmulet)
                    CurrentMap.PlaceMapCharacter(MapLevel.AMULET, false);
            }
        }

        public void MoveCharacter(Player player, MapLevel.Direction direct)
        {

            List<char> charsAllowed =
                new List<char>(){MapLevel.ROOM_INT, MapLevel.STAIRWAY,
                MapLevel.ROOM_DOOR, MapLevel.HALLWAY , MapLevel.ENEMY};
            List<char?> charsEvent = new List<char?>() { MapLevel.ENEMY }; ;

            List<char> charAllowed =
                new List<char>(){MapLevel.ROOM_INT, MapLevel.STAIRWAY,
                //MapLevel.ROOM_DOOR, MapLevel.HALLWAY , MapLevel.ENEMY};
                MapLevel.ROOM_DOOR, MapLevel.HALLWAY , MapLevel.DEALER};
            //List<char?> charsEvent = new List<char?>() { MapLevel.ENEMY };
            List<char?> charEvent = new List<char?>() { MapLevel.DEALER };


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

            if (player.Location.ItemCharacter == MapLevel.GOLD)
                PickUpGold();
            else if (player.Location.ItemCharacter != null)
                AddInventory();

        }

        private void PickUpGold()
        {
            int goldAmt = rand.Next(MapLevel.MIN_GOLD_AMT, MapLevel.MAX_GOLD_AMT);
            CurrentPlayer.Gold += goldAmt;

            CurrentPlayer.Location.ItemCharacter = null;
            //Сообщение $"You picked up {goldAmt} pieces of gold.";
        }

        private string AddInventory()
        {
            string retValue = "";

            if (CurrentPlayer.Location!.ItemCharacter == MapLevel.AMULET)
            {
                CurrentPlayer.HasAmulet = true;
                CurrentPlayer.Location!.ItemCharacter = null;
                //retValue = "You found the Amulet of Yendor!  It has been added to your inventory.";
            }

            return retValue;
        }
    }

}