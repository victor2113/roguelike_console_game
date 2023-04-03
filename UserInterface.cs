namespace RogueFefu
{
    internal class UserInterface
    {
        public UserInterface(string pMap, string pStatus, StartMenu pMenu)
        {
            Map = pMap;
            Status = pStatus;
            Menu = pMenu;
            RedrawScreen();
        }

        public void UpdateUi(string pMap, string pStatus, StartMenu pMenu)
        {
            Map = pMap;
            Status = pStatus;
            Menu = pMenu;
            RedrawScreen();
        }

        public static void ConsoleWriteln(string message, int left=0, int top=0)
        {
            Console.SetCursorPosition(left, top);
            Console.WriteLine(message);
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
            Console.ForegroundColor = ConsoleColor.Green;
            ConsoleWriteln("╔══════════════════════════════════════════════════════════════════════════════╗", 0, MapHeight + 1);
            ConsoleWriteln($"║ {s}{new string(' ', MapWidth - s.Length - 3)}║", 0, MapHeight + 2);
            ConsoleWriteln("╚══════════════════════════════════════════════════════════════════════════════╝", 0, MapHeight + 3);
        }

        public string Map;
        public string Status;
        public StartMenu Menu;
        private const int MapWidth = 80;
        private const int MapHeight = 25;
    }
}
