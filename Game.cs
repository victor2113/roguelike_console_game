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
        public Player CurrentPlayer { get; }
        public int CurrentTurn { get; set; }
        public Battle battlelvl { get; set; }
        public Enemy CurrentEnemy { get; set; }
        public Dealer dealer { get; set; }

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
            this.CurrentMap = new MapLevel();
            this.CurrentPlayer = new Player(PlayerName);
            this.CurrentPlayer.Location = CurrentMap.PlaceMapCharacter(Player.CHARACTER, true);
        }

        public Game(string PlayerName)
        {
            this.CurrentMap = new MapLevel();
            this.CurrentPlayer = new Player(PlayerName);
            this.CurrentPlayer.Location = CurrentMap.PlaceMapCharacter(Player.CHARACTER, true);
            this.battlelvl = new Battle();
            this.CurrentEnemy = new Enemy();
            this.dealer = new Dealer();
        }
        public void Begin()
        {
            string logo = $"{GameLogo}\n\n" + $"Hello, {this.CurrentPlayer.PlayerName}! Let's start the game!\n";

            Console.CursorVisible = false;
            string[] options = { "Start", "About", "Exit" };
            StartMenu startMenu = new StartMenu(options);
            UserInterface ui = new UserInterface(logo, "Use the arrow keys to choose options and press enter to select one", CurrentPlayer);
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
            UserInterface ui = new UserInterface(GameLogo, "Press any key to exit....", CurrentPlayer);
            Console.ReadKey(true);
            Environment.Exit(0);
        }
        private void AboutGameText()
        {
            UserInterface ui = new UserInterface($"{GameLogo}\n\nBla bla bla", "Press any key to return", CurrentPlayer);
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
            bool startTurn = false;
            UserInterface ui = new UserInterface(LoadMapLevel(), "Use arrows keys to walk, ESC to exit game.", CurrentPlayer);
            do
            {
                int currX = CurrentPlayer.Location.X;
                int currY = CurrentPlayer.Location.Y;
                int key = ReadKeyPressedByPlayer();
                switch (key)
                {
                    case ARROWUP:
                        MoveCharacter(CurrentPlayer, MapLevel.Direction.North);
                        break;
                    case ARROWDOWN:
                        MoveCharacter(CurrentPlayer, MapLevel.Direction.South);
                        break;
                    case ARROWLEFT:
                        MoveCharacter(CurrentPlayer, MapLevel.Direction.West);
                        break;
                    case ARROWRIGHT:
                        MoveCharacter(CurrentPlayer, MapLevel.Direction.East);
                        break;
                    case ESCAPE:
                        ExitGame();
                        break;
                }
                if (key == 7)
                {
                    startTurn = true;
                    if (CurrentPlayer.Location.MapCharacter == MapLevel.STAIRWAY)
                        ChangeLevel(1);
                    //else
                    //"There's no stairway here.";
                }
                else
                {


                }
                if (CurrentPlayer.Location.X != currX || CurrentPlayer.Location.Y != currY)
                    ui.UpdateUi(LoadMapLevel(), "Use arrows keys to walk, ESC to exit game.", ui.Gamer);
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
            else if (keyPressed == ConsoleKey.OemComma)
            {
                ButtonPressed = 5;
            }
            else if (keyPressed == ConsoleKey.OemPeriod)
            {
                ButtonPressed = 6;
            }
            else if (keyPressed == ConsoleKey.L)
            {
                ButtonPressed = 7;
            }
            return ButtonPressed;
        }

        private void ChangeLevel(int Change)
        {
            bool allowPass = false;

            if (Change > 0)
            {
                allowPass = CurrentPlayer.HasAmulet && CurrentPlayer.Level >= 1;
                // "You cannot go that way.";
            }

            if (allowPass)
            {
                CurrentMap = new MapLevel();
                CurrentPlayer.Level += Change;
                CurrentPlayer.HasAmulet = false;
                CurrentPlayer.Location = CurrentMap.PlaceMapCharacter(Player.CHARACTER, true);

                if (CurrentPlayer.Level == MAX_LEVEL && !CurrentPlayer.HasAmulet)
                    CurrentMap.PlaceMapCharacter(MapLevel.AMULET, false);
            }
        }

        public void MoveCharacter(Player player, MapLevel.Direction direct)
        {

            List<char> charsAllowed =
                new List<char>(){MapLevel.ROOM_INT, MapLevel.STAIRWAY,MapLevel.AMULET,
                MapLevel.ROOM_DOOR, MapLevel.HALLWAY , MapLevel.ENEMY , MapLevel.DEALER};
            List<char?> charsEvent = new List<char?>() { MapLevel.ENEMY, MapLevel.DEALER }; ;





            Dictionary<MapLevel.Direction, MapSpace> surrounding =
                CurrentMap.SearchAdjacent(player.Location.X, player.Location.Y);

            if (charsEvent.Contains(surrounding[direct].ItemCharacter))
            {
                if (surrounding[direct].ItemCharacter == MapLevel.ENEMY)
                    battlelvl.Begin(CurrentPlayer, CurrentEnemy);
                if (surrounding[direct].ItemCharacter == MapLevel.DEALER)
                    dealer.ItemsList(player);
            }


            if (charsAllowed.Contains(surrounding[direct].MapCharacter) &&
                surrounding[direct].DisplayCharacter == null)
            {
                player.Location = CurrentMap.MoveDisplayItem(player.Location, surrounding[direct]);
                if (player.Location.ItemCharacter == MapLevel.ENEMY && player.runAway == false)
                {
                    player.Location.ItemCharacter = null;
                }
                    

            }




            if (player.Location.ItemCharacter == MapLevel.GOLD)
                PickUpGold();
            else if (player.Location.ItemCharacter == MapLevel.AMULET)
                AddInventory();


        }

        private void PickUpGold()
        {
            int goldAmt = rand.Next(MapLevel.MIN_GOLD_AMT, MapLevel.MAX_GOLD_AMT);
            CurrentPlayer.Gold += goldAmt;

            CurrentPlayer.Location.ItemCharacter = null;

        }

        private void AddInventory()
        {
            CurrentPlayer.HasAmulet = true;
            CurrentPlayer.Location!.ItemCharacter = null;
            //"You found the Amulet of Yendor!  It has been added to your inventory.";
        }
    }

}