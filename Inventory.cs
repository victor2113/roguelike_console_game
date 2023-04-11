using System;
namespace RogueFefu
{
    internal class Inventory
    {
        public bool InventOver;
        public int addHP = 10;
        public int addStrength = 5;

        public void ItemInventory(Dealer dealer, Player player)
        {
            Console.CursorVisible = false;
            string[] invent = { @"
╔══════════════════════════════════════════════════════════════════════════════════════════════════╗
║                                                                                                  ║
║                                                                                                  ║
║                               ⠀⠀⠀⠀⠀⠀⣀⣀⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀                                     ║
║                               ⠀⠀⠀⠀⠀⠺⠿⠿⠿⠿⢿⣿⣿⣶⣶⣶⣶⣤⣤⣤⣤⣄⣀⣀⡀⠀⠀⠀⠀⠀                                     ║
║                               ⠀⠀⠀⠀⠀⢾⣷⡆⢰⣶⣦⣤⣤⣤⣤⣉⣉⣉⣉⠙⠛⠛⠛⠛⢠⣿⣷⠀⠀⠀                                     ║
║                               ⠀⠀⠀⠀⠀⠘⣿⡇⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⠈⣿⣿⠀⠀⠀                                     ║
║                               ⠀⠀⠀⠀⠀⠀⠸⠇⣸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣇⠸⣿⠀⠀⠀                                     ║
║                               ⠀⠀⠀⠀⠀⠀⠀⠀⠿⠿⠿⠿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡄⠻⠀⠀⠀                                     ║
║                               ⠀⠀⠀⠀⠀⢀⡄⢰⣶⣶⣶⣶⣤⣤⣤⣤⣈⣉⣉⣉⡙⠛⠛⠛⠛⠷⠀⠀⠀⠀                                     ║
║                               ⠀⠀⠀⠀⠾⠿⡇⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠗⢀⠀⠀⠀⠀                                     ║
║                               ⠀⠀⠀⣶⣶⣶⣤⣤⣤⣤⣈⣉⣉⣉⡙⠛⠛⠛⠛⠿⠿⠿⠛⣁⣴⣿⠀⠀⠀⠀                                     ║
║                               ⠀⠀⠀⣿⣿⣿⣿⣿⣿⣿⣿⣟⠉⣿⣿⣿⣿⣿⣿⣶⣶⡆⢸⣿⣿⣿⠀⠀⠀⠀                                     ║    
║                               ⠀⠀⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣼⣿⣿⣿⣿⣿⣿⣿⣿⡇⢸⣿⣿⣿⠀⠀⠀⠀                                     ║
║                               ⠀⠀⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⢸⣿⣿⣿⠀⠀⠀⠀                                     ║    
║                               ⠀⠀⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⢸⣿⣿⡿⠀⠀⠀⠀                                     ║
║                               ⠀⠀⠀⠉⠉⠉⠙⠛⠛⠛⠻⠿⠿⠿⠿⣿⣿⣿⣿⣿⣿⡇⢸⠟⠁⠀⠀⠀⠀⠀                                     ║
║                               ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠉⠁⠀⠀⠀⠀⠀⠀⠀⠀                                     ║
║                                                                                                  ║
╚══════════════════════════════════════════════════════════════════════════════════════════════════╝" };
            string[] items = { "Exit", "", "", "" };
            string promt = invent[0];

            if (dealer.countArmor >= 1)
                items[1] = $"Armor: {dealer.countArmor}";
            if (dealer.countWeapon >= 1)
                items[2] = $"Weapon: {dealer.countWeapon}";
            if (player.HasAmulet)
                items[3] = $"Amulet of Tolik";
            StartMenu InventMenu = new StartMenu(items);
            UserInterface ui = new UserInterface($"\n{promt}", "Select an item to use or get information", player);
            InventOver = false;
            do
            {
                int selectedIndex = InventMenu.Run();
                switch (selectedIndex)
                {
                    case 0:
                        Exit(ui);
                        break;
                    case 1:
                        UseArmor(player, dealer, ui);
                        break;
                    case 2:
                        UseWeapon(player, dealer, ui);
                        break;
                    case 3:
                        AmuletInstruction(player, ui);
                        break;
                }
                ui.UpdateUi(ui.Map, "Select an item to use or get information", player);
            } while (InventOver != true);
        }

        private void UseArmor(Player player, Dealer dealer, UserInterface ui)
        {
            if (dealer.countArmor >= 1)
            {
                player.HP += addHP;
                dealer.countArmor -= 1;
                player.HasArmor = true;
                ui.UpdateUi(ui.Map, $"You use an Armor, your Health promoted by {addHP}.", ui.Gamer);
            }
            Console.ReadKey(true);
        }

        private void UseWeapon(Player player, Dealer dealer, UserInterface ui)
        {
            if (dealer.countWeapon >= 1)
            {
                player.Strength += addStrength;
                dealer.countWeapon -= 1;
                player.HasWeapon = true;
                ui.UpdateUi(ui.Map, $"You use a Weapon, your Strength promoted by {addStrength}.", ui.Gamer);
            }
            Console.ReadKey(true);
        }

        private void AmuletInstruction(Player player, UserInterface ui)
        {
            if (player.HasAmulet)
            {
                ui.UpdateUi(ui.Map, "Now you can Level Up. Find Stairway.", ui.Gamer);
            }
            Console.ReadKey(true);
        }

        private void Exit(UserInterface ui)
        {
            ui.UpdateUi(ui.Map, "Press any key to exit.", ui.Gamer);
            InventOver = true;
            Console.ReadKey(true);
        }
    }

}
