using System;
namespace RogueFefu
{
    internal class Inventory
    {
        public bool InventOver;
        public int addHP = 10;
        public int addStrength = 5;

        public void ItemInventory(Dealer dealer, Battle battle, Player player)
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
                        UseArmor(player, dealer, battle, ui);
                        break;
                    case 2:
                        UseWeapon(player, dealer, battle, ui);
                        break;
                    case 3:
                        AmuletInstruction(ui);
                        break;
                }
                ui.UpdateUi(ui.Map, "bla bla", player);
            } while (InventOver != true);
        }

        private void UseArmor(Player player, Dealer dealer, Battle battle, UserInterface ui)
        {
            player.HP += addHP;
            if (battle.countDamage == 7)
            {
                dealer.countArmor -= 1;
                battle.countDamage = 0;
            }
            ui.UpdateUi(ui.Map, $"You use an Armor, your Health promoted by {addHP}.", ui.Gamer);
            Console.ReadKey(true);
        }

        private void UseWeapon(Player player, Dealer dealer, Battle battle, UserInterface ui)
        {
            player.Strength += addStrength;
            if (battle.countHit == 5)
            {
                dealer.countWeapon -= 1;
                battle.countHit = 0;
            }
            ui.UpdateUi(ui.Map, $"You use a Weapon, your Strength promoted by {addStrength}.", ui.Gamer);
            Console.ReadKey(true);
        }

        private void AmuletInstruction(UserInterface ui)
        {
            ui.UpdateUi(ui.Map, "Now you can Level Up. Find Stairway.", ui.Gamer);
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

