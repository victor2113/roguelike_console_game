﻿using System;
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
        public int priceExp = 35;
        public int addHP = 10;//
        public int priceArmor = 30;
        public int countArmor = 0;
        public int addStrength = 5;//
        public int priceWeapon = 25;
        public int countWeapon = 0;
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
            UserInterface ui = new UserInterface($"\n{promt}", $"Welcome, {player.PlayerName}! Please buy some items here.", player);
            DealOver = false;
            do
            {
                int selectedIndex = DealerMenu.Run();
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
                ui.UpdateUi(ui.Map, $"Welcome, {player.PlayerName}! Please buy some items here.", player);
            } while (DealOver != true);
        }

        private void BuyExperience(Player player, UserInterface ui)
        {
            if (player.Gold < priceExp)
            {
                ui.UpdateUi(ui.Map, "Not enough Gold", ui.Gamer);
                if (player.Gold < priceWeapon)
                {
                    DealOver = true;
                }
            }
            else
            {
                player.Experience += addExp;
                player.Gold -= priceExp;
                ui.UpdateUi(ui.Map, $"Experience promoted by {addExp}. Press a key to continue.", ui.Gamer);
                Console.ReadKey(true);
                if (player.Experience >= 10)
                {
                    ui.UpdateUi(ui.Map, $"Find the amulet to Level Up.", ui.Gamer);
                    if (player.HasAmulet == true)
                    {
                        ui.UpdateUi(ui.Map, $"Now you can Level Up! Find Stairway.", ui.Gamer);
                    }
                }
            }
            Console.ReadKey(true);
        }

        private void BuyArmor(Player player, UserInterface ui)
        {
            if (player.Gold < priceArmor)
            {
                ui.UpdateUi(ui.Map, "Not enough Gold", ui.Gamer);
                if (player.Gold < priceWeapon)
                {
                    DealOver = true;
                }
            }
            else
            {
                player.Gold -= priceArmor;
                countArmor += 1;
                ui.UpdateUi(ui.Map, $"You bought an Armor.", player);
            }
            Console.ReadKey(true);
        }

        private void BuyWeapon(Player player, UserInterface ui)
        {
            if (player.Gold < priceWeapon)
            {
                ui.UpdateUi(ui.Map, "Not enough Gold", ui.Gamer);
                DealOver = true;
            }
            else
            {
                player.Gold -= priceWeapon;
                countWeapon += 1;
                ui.UpdateUi(ui.Map, $"You bought a Weapon.", player);
            }
            Console.ReadKey(true);
        }

        private void Exit(UserInterface ui)
        {
            ui.UpdateUi(ui.Map, "Press any key to exit.", ui.Gamer);
            DealOver = true;
            Console.ReadKey(true);
        }
    }
}