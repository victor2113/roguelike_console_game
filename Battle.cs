using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace RogueFefu
{
    internal class Battle
    {
        public string GameOver = @"
   _________    __  _________   ____ _    ____________ 
  / ____/   |  /  |/  / ____/  / __ \ |  / / ____/ __ \
 / / __/ /| | / /|_/ / __/    / / / / | / / __/ / /_/ /
/ /_/ / ___ |/ /  / / /___   / /_/ /| |/ / /___/ _, _/ 
\____/_/  |_/_/  /_/_____/   \____/ |___/_____/_/ |_|  
                                                       
";

        public bool BattleOver;
        private static Random rand = new Random();
        public void Begin(Player player, Enemy enemy)
        {
            Console.CursorVisible = false;
            string[] enemies = { @"
╔══════════════════════════════════════════════════════════════════════════════════════════════════╗
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                        /\                 /\                                     ║
║                                       / \'._   (\_/)   _.'/ \                                    ║
║                                       |.''._'--(o.o)--'_.''.|                                    ║
║                                        \_ / `;=/ ' \=;` \ _/                                     ║
║                                          `\__| \___/ |__/`                                       ║
║                                               \(_|_)/                                            ║
║                                               '' ` ''                                            ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
╚══════════════════════════════════════════════════════════════════════════════════════════════════╝", @"
╔══════════════════════════════════════════════════════════════════════════════════════════════════╗
║                                                                                                  ║
║                                                   .WWWW.                                         ║
║                                                  WWWWw`w`'                                       ║
║                                                .WWWW O O                                         ║
║                                             .WWWW''WW.'-.                                        ║
║                                            WWWWWWWWWWWWW.                                        ║
║                                           WWWWWWWWWWWWWWW                                        ║
║                                           ''WWWWWWWWWW''\___                                     ║
║                                            /  /__ __/\___( \                                     ║
║                                           (____( \X(     /||\                                    ║
║                                              / /||\ \                                            ║
║                                              \______/                                            ║
║                                               \ | \ |                                            ║
║                                                )|  \|                                            ║
║                                               (_|  /|                                            ║
║                                               |X| (X|                                            ║
║                                               |X| |X'._                                          ║
║                                              (__| (____)                                         ║
╚══════════════════════════════════════════════════════════════════════════════════════════════════╝", @"
╔══════════════════════════════════════════════════════════════════════════════════════════════════╗
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║               (^_^)                                                                              ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                               __.-._                                             ║
║                                               '-._''7'                                           ║
║                                                /'.-c                                             ║
║                                                |  /T                                             ║
║                                               _)_/LI                                             ║
╚══════════════════════════════════════════════════════════════════════════════════════════════════╝", @"
╔══════════════════════════════════════════════════════════════════════════════════════════════════╗
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║               (^_^)                                                                              ║
║                                                                                                  ║
║                                                  O                                               ║
║                                                 _|_                                              ║
║                                           ,_.-_' _ '_-._,                                        ║
║                                            \ (')(.)(') /                                         ║
║                                         _,  `\_-===-_/`  ,_                                      ║
║                                        >  |----''*''----|  <                                     ║
║                                        `**`--/   _-@-\--`**`                                     ║
║                                             |===L_I===|                                          ║
║                                              \       /                                           ║
║                                              _\__|__/_                                           ║
║                                             `wwww`wwww`                                          ║
╚══════════════════════════════════════════════════════════════════════════════════════════════════╝", @"
╔══════════════════════════════════════════════════════════════════════════════════════════════════╗
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║               (^_^)                                                                              ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                  //                                              ║
║                                                 (oo\                                             ║
║                                                 / ._)                                            ║
║                                                J _=\=                                            ║
║                                                |   /                                             ║
║                                           3._.' |_|_                                             ║
╚══════════════════════════════════════════════════════════════════════════════════════════════════╝", @"
╔══════════════════════════════════════════════════════════════════════════════════════════════════╗
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║               (^_^)                                                                              ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                 &&&                                              ║
║                                                (+.+)                                             ║
║                                              ___\=/___                                           ║
║                                             (|_ ~~~ _|)                                          ║
║                                                |___|                                             ║
║                                                / _ \                                             ║
║                                               /_/ \_\                                            ║
║                                              /_)   (_\                                           ║
╚══════════════════════════════════════════════════════════════════════════════════════════════════╝" };

            string promt = enemies[new Random().Next(0, enemies.Length)];
            string[] options = { "Attack", "Defend", "Run away" };
            StartMenu BattleMenu = new StartMenu(options);
            /*if (flagLevelUp == 1)
            {
                player.HP += 5;
                player.Strength += 5;
            }*/
            UserInterface ui = new UserInterface($"\n{promt}", $"Battle! Enemy stats: HP: {enemy.HP}, Strength: {enemy.Strength}", player);
            BattleOver = false;
            do
            {
                int selectedIndex = BattleMenu.Run();
                switch (selectedIndex)
                {
                    case 0:
                        Attack(player, enemy, ui);
                        break;
                    case 1:
                        Defend(player, enemy, ui);
                        break;
                    case 2:
                        RunAway(player , enemy, ui);
                        break;
                }
                ui.UpdateUi(ui.Map, $"Battle is ongoing... Enemy HP: {enemy.HP}", player);
            } while (!BattleOver);
        }
        public int mod = 0;
        private void Attack(Player player, Enemy enemy, UserInterface ui)
        {
            ui.UpdateUi(ui.Map, "You attacked the enemy.", ui.Gamer);
            player.HP = player.HP - enemy.Strength;
            enemy.HP = enemy.HP - player.Strength;
            if (player.HP <= 0)
            {
                ui.UpdateUi(GameOver, "You lost! Press any key to continue...", ui.Gamer);
                player.runAway = false;
                BattleOver = true;
            }

            if (enemy.HP <= 0)
            {
                ui.UpdateUi(ui.Map, "You won! All stats and also experience are promoted by 4.", ui.Gamer);
                player.HP += 4;
                player.Strength += 4;
                player.Gold += 20;
                if ((player.Experience + 4) < 5)
                {
                    player.Experience += 4;
                }
                else if ((player.Experience + 4) >= 5)
                {
                    mod = player.Experience + 4 - 5;
                    player.Experience = mod;
                    player.Level += 1;
                    player.Strength += 5;
                    player.HP += 5;
                    ui.UpdateUi(ui.Map, $"Level Up! Hero damage and health are promoted by 5.", ui.Gamer);
                }
                mod = 0;
                BattleOver = true;
                enemy.HP = rand.Next(40, 90);
                enemy.Strength = rand.Next(2, 6);
            }
            if (BattleOver != true)
            {
                ui.UpdateUi(ui.Map, $"{ui.Status}", ui.Gamer);
            }
            Console.ReadKey(true);
        }

        private void Defend(Player player, Enemy enemy, UserInterface ui)
        {
            ui.UpdateUi(ui.Map, "You used a shield. Press any key to continue...", ui.Gamer);
            Console.ReadKey(true);
        }

        private void RunAway(Player player , Enemy enemy, UserInterface ui)
        {
            ui.UpdateUi(ui.Map, "Press any key to run away", ui.Gamer);
            BattleOver = true;
            player.runAway = true;
            enemy.HP = rand.Next(40, 90);
            enemy.Strength = rand.Next(2, 6);
            Console.ReadKey(true);
        }
    }
}