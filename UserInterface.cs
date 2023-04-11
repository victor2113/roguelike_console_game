namespace RogueFefu
{
    /*
     Класс UserInterface
     ===================
     1. string Map: отображается в области (0,0) - (101,25). предназначено для отображения карты,
     логотипа игры, боёвки, торговца и тд.
     2. string Status: предназначено для отображения информационных сообщений для игрока 
     3. Palyer Gamer: предназначено для вывода статусной иноформации об игроке
     
     для обновления экрана игры можно использовать как конструктор
     UserInterface ui = new Userinterface(....)

     так и метод UpdateUi, например, ui.UpdateUi(...)
     */

    internal class UserInterface
    {
        public UserInterface(string pMap, string pStatus, Player pGamer)
        {
            Map = pMap;
            Status = pStatus;
            Gamer = pGamer;
            RedrawScreen();
        }

        public void UpdateUi(string pMap, string pStatus, Player pGamer)
        {
            Map = pMap;
            Status = pStatus;
            Gamer = pGamer;
            RedrawScreen();
        }

        public static void ConsoleWriteln(string message, int left = 0, int top = 0)
        {
            Console.SetCursorPosition(left, top);
            Console.Write(message);
        }

        public void RedrawScreen()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            ConsoleWriteln(Map);
            WriteStatus(Status);
        }

        private void WriteStatus(string s)
        {
            Console.SetCursorPosition(0, MapHeight);
            if (Gamer != null)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"<{Gamer.PlayerName}> ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"HP: {Gamer.HP} Damage: {Gamer.Strength} Experience: {Gamer.Experience} Level: {Gamer.Level} ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write($"Gold: {Gamer.Gold}");
            }

            Console.ForegroundColor = ConsoleColor.Green;
            ConsoleWriteln($"{new string('*', MapWidth)}", 0, MapHeight + 1);
            ConsoleWriteln($"* ", 0, MapHeight + 2);
            Console.ForegroundColor = ConsoleColor.White;
            ConsoleWriteln($"{s}{new string(' ', MapWidth - s.Length - 3)}", 2, MapHeight + 2);
            Console.ForegroundColor = ConsoleColor.Green;
            ConsoleWriteln("*", MapWidth - 1, MapHeight + 2);
            ConsoleWriteln($"{new string('*', MapWidth)}", 0, MapHeight + 3);
        }

        public string Map;
        public string Status;
        public Player Gamer;
        public Game Level;
        public const int MapWidth = 101;
        public const int MapHeight = 25;
    }
}
