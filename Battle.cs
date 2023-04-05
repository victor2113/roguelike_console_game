using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace RogueFefu
{
    internal class Battle
    {
        public bool BattleOver;
        public void Begin(Player player, Enemy enemy)
        {
            Console.CursorVisible = false;
            string[] enemies = { @"
╔═════════════════════════════════════════════════════════════════════════════════════════════════════════╗
║                                                                                                         ║
║                                                                                                         ║
║                                                                                                         ║
║                                                                                                         ║
║                                                                                                         ║
║                                                                                                         ║
║                                            /\                 /\                                        ║
║                                           / \'._   (\_/)   _.'/ \                                       ║
║                                           |.''._'--(o.o)--'_.''.|                                       ║
║                                            \_ / `;=/ ' \=;` \ _/                                        ║
║                                              `\__| \___/ |__/`                                          ║
║                                                   \(_|_)/                                               ║
║                                                   '' ` ''                                               ║
║                                                                                                         ║
║                                                                                                         ║
║                                                                                                         ║
║                                                                                                         ║
║                                                                                                         ║
╚═════════════════════════════════════════════════════════════════════════════════════════════════════════╝", @"
╔═════════════════════════════════════════════════════════════════════════════════════════════════════════╗
║                                                                                                         ║
║                                                       .WWWW.                                            ║
║                                                      WWWWw`w`'                                          ║
║                                                    .WWWW O O                                            ║
║                                                 .WWWW''WW.'-.                                           ║
║                                                WWWWWWWWWWWWW.                                           ║
║                                               WWWWWWWWWWWWWWW                                           ║
║                                               ''WWWWWWWWWW''\___                                        ║
║                                                /  /__ __/\___( \                                        ║
║                                               (____( \X(     /||\                                       ║
║                                                  / /||\ \                                               ║
║                                                  \______/                                               ║
║                                                   \ | \ |                                               ║
║                                                    )|  \|                                               ║
║                                                   (_|  /|                                               ║
║                                                   |X| (X|                                               ║
║                                                   |X| |X'._                                             ║
║                                                  (__| (____)                                            ║
╚═════════════════════════════════════════════════════════════════════════════════════════════════════════╝", @"
╔═════════════════════════════════════════════════════════════════════════════════════════════════════════╗
║                                                                                                         ║
║                                                                                                         ║
║                                                                                                         ║
║                                                                                                         ║
║                                                                                                         ║
║                   (^_^)                                                                                 ║
║                                                                                                         ║
║                                                                                                         ║
║                                                                                                         ║
║                                                                                                         ║
║                                                                                                         ║
║                                                                                                         ║
║                                                                                                         ║
║                                                   __.-._                                                ║
║                                                   '-._''7'                                              ║
║                                                    /'.-c                                                ║
║                                                    |  /T                                                ║
║                                                   _)_/LI                                                ║
╚═════════════════════════════════════════════════════════════════════════════════════════════════════════╝", @"
╔═════════════════════════════════════════════════════════════════════════════════════════════════════════╗
║                                                                                                         ║
║                                                                                                         ║
║                                                                                                         ║
║                                                                                                         ║
║                                                                                                         ║
║                   (^_^)                                                                                 ║
║                                                                                                         ║
║                                                      O                                                  ║
║                                                     _|_                                                 ║
║                                               ,_.-_' _ '_-._,                                           ║
║                                                \ (')(.)(') /                                            ║
║                                             _,  `\_-===-_/`  ,_                                         ║
║                                            >  |----''*''----|  <                                        ║
║                                            `**`--/   _-@-\--`**`                                        ║
║                                                 |===L_I===|                                             ║
║                                                  \       /                                              ║
║                                                  _\__|__/_                                              ║
║                                                 `wwww`wwww`                                             ║
╚═════════════════════════════════════════════════════════════════════════════════════════════════════════╝", @"
╔═════════════════════════════════════════════════════════════════════════════════════════════════════════╗
║                                                                                                         ║
║                                                                                                         ║
║                                                                                                         ║
║                                                                                                         ║
║                                                                                                         ║
║                                                                                                         ║
║                   (^_^)                                                                                 ║
║                                                                                                         ║
║                                                                                                         ║
║                                                                                                         ║
║                                                                                                         ║
║                                                                                                         ║
║                                                      //                                                 ║
║                                                     (oo\                                                ║
║                                                     / ._)                                               ║
║                                                    J _=\=                                               ║
║                                                    |   /                                                ║
║                                               3._.' |_|_                                                ║
╚═════════════════════════════════════════════════════════════════════════════════════════════════════════╝", @"
╔═════════════════════════════════════════════════════════════════════════════════════════════════════════╗
║                                                                                                         ║
║                                                                                                         ║
║                                                                                                         ║
║                                                                                                         ║
║                                                                                                         ║
║                                                                                                         ║
║                   (^_^)                                                                                 ║
║                                                                                                         ║
║                                                                                                         ║
║                                                                                                         ║
║                                                     &&&                                                 ║
║                                                    (+.+)                                                ║
║                                                  ___\=/___                                              ║
║                                                 (|_ ~~~ _|)                                             ║
║                                                    |___|                                                ║
║                                                    / _ \                                                ║
║                                                   /_/ \_\                                               ║
║                                                  /_)   (_\                                              ║
╚═════════════════════════════════════════════════════════════════════════════════════════════════════════╝" };

            string promt = enemies[new Random().Next(0, enemies.Length)];
            string[] options = { "Attack", "Defend", "Run away" };
            StartMenu BattleMenu = new StartMenu(options, promt);
            int selectedIndex = BattleMenu.Run();
            do
            {
                switch (selectedIndex)
                {
                    case 0:
                        Attack(player, enemy);
                        break;
                    case 1:
                        Defend(player, enemy);
                        break;
                    case 2:
                        RunAway();
                        break;
                }
            } while (!BattleOver);
            Console.ReadKey(true);
        }
        private void Attack(Player player, Enemy enemy)
        {;
            Console.WriteLine("You've attacked the enemy");
            player.HP = player.HP - enemy.Strength;
            enemy.HP = enemy.HP - player.Strength;
            if (player.HP <= 0)
            {
                Console.WriteLine(@"

   _________    __  _________   ____ _    ____________ 
  / ____/   |  /  |/  / ____/  / __ \ |  / / ____/ __ \
 / / __/ /| | / /|_/ / __/    / / / / | / / __/ / /_/ /
/ /_/ / ___ |/ /  / / /___   / /_/ /| |/ / /___/ _, _/ 
\____/_/  |_/_/  /_/_____/   \____/ |___/_____/_/ |_|  
                                                       
");
                BattleOver = true;
            }
            if (enemy.HP <= 0)
            {
                Console.WriteLine("You won!");
                BattleOver = true;
            }
            if (BattleOver != true)
            {
                Console.WriteLine("Your HP: " + player.HP);
                Console.WriteLine("Enemy HP: " + enemy.HP);
            }
            Console.ReadKey(true);
        }
        private void Defend(Player player, Enemy enemy)
        {
            Console.WriteLine("\nYou've used a shield");
            Console.ReadKey(true);
        }
        private void RunAway()
        {
            Console.WriteLine("\nPress any key to run away");
            Console.ReadKey(true);
        }
    }
}