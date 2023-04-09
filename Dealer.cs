using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RogueFefu
{
    internal class Dealer
    {
        public int addExp = 1;
        public int priceExp = 5;
        public int addStrength = 5;
        public int priceWeapon = 10;
        public int addHP = 10;
        public int priceArmor = 15;
        public bool DealOver;
        public void ItemsList(Player player)
        {
            Console.CursorVisible = false;
            string[] dealer = { @"
╔══════════════════════════════════════════════════════════════════════════════════════════════════╗
║                                              .-.                                                 ║
║                                             [.-''-.,                                             ║
║                                             |  //`~\)                                            ║
║                                             (<| 0\0|>                                            ║
║                                             ';\  _'/                                             ║
║                                            __\|'._/_                                             ║
║                                           /\ \    || )_///_\>>                                   ║
║                                          (  '._ T |\ | _/),-'                                    ║
║                                           '.   '._.-' /'/ |                                      ║
║                                           | '._   _.'`-.._/                                      ║
║                                           ,\ / '-' |/                                            ║
║                                           [_/\-----j                                             ║
║                                      _.--.__[_.--'_\__                                           ║
║                                     /         `--'    '---._                                     ║
║                                    /  '---.  -'. .'  _.--   '.                                   ║
║                                    \_      '--.___ _;.-o     /                                   ║
║                                      '.__ ___/______.__8----'                                    ║
║                                        c-'----'                                                  ║
╚══════════════════════════════════════════════════════════════════════════════════════════════════╝" };
            string[] items = { $"Experience: {priceExp}", $"Armor: {priceArmor}", $"Weapon: {priceWeapon}", "Exit" };
            string promt = dealer[0];

            StartMenu DealerMenu = new StartMenu(items);
            UserInterface ui = 
                new UserInterface($"\n{promt}", $"Welcome {player.PlayerName}! Please buy some items here.", player);

            int selectedIndex = DealerMenu.Run();
            DealOver = false;
            switch (selectedIndex)
            {
                case 0:
                    BuyExperience(player, ui);
                    break;
                case 1:
                    BuyArmor(player, ui);
                    break;
                case 2:
                    BuyWeapon(player, ui);
                    break;
                case 3:
                    Exit(ui);
                    break;
            }
            ui.UpdateUi(ui.Map, $"Welcome {player.PlayerName}! Please buy some items here.", player);
        }

        private void BuyExperience(Player player, UserInterface ui)
        {
            if (player.Gold < priceExp)
            {
                ui.UpdateUi(ui.Map, "Not enough Gold", ui.Gamer);
            }
            else
            {
                player.Experience += addExp;
                player.Gold -= priceExp;
                ui.UpdateUi(ui.Map, $"You bought an Experience. Press a key to continue.", ui.Gamer);
            }
            Console.ReadKey(true);
            ui.UpdateUi(ui.Map, $"Welcome {player.PlayerName}! Please buy some items here.", player);
        }

        private void BuyArmor(Player player, UserInterface ui)
        {
            if (player.Gold < priceArmor)
            {
                ui.UpdateUi(ui.Map, "Not enough Gold", ui.Gamer);
            }
            else
            {
                player.HP += addHP;
                player.Gold -= priceArmor;
                ui.UpdateUi(ui.Map, $"You bought an Armor. Press a key to continue.", ui.Gamer);
            }
            Console.ReadKey(true);
            ui.UpdateUi(ui.Map, $"Welcome {player.PlayerName}! Please buy some items here.", player);
        }

        private void BuyWeapon(Player player, UserInterface ui)
        {
            if (player.Gold < priceWeapon)
            {
                ui.UpdateUi(ui.Map, "Not enough Gold", ui.Gamer);
            }
            else
            {
                player.Strength += addStrength;
                player.Gold -= priceWeapon;
                ui.UpdateUi(ui.Map, $"You bought a Weapon. Press a key to continue.", ui.Gamer);
            }
            Console.ReadKey(true);
            ui.UpdateUi(ui.Map, $"Welcome {player.PlayerName}! Please buy some items here.", player);
        }

        private void Exit(UserInterface ui)
        {
            ui.UpdateUi(ui.Map, "Press any key to exit", ui.Gamer);
            DealOver = true;
            Console.ReadKey(true);
        }
    }
}