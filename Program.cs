using System;


namespace RogueFefu
{
    class Program
    {
        static void Main(string[] args)
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

            Game game = new Game(playerName);
            game.Begin();

        }
    }
}