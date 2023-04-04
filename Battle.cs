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
        public void Begin()
        {
            Console.CursorVisible = false;
            string[] enemy = { @"
╔══════════════════════════════════════════════════════════════════════════════════════════════════╗
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                         /\                 /\                                    ║
║                                        / \'._   (\_/)   _.'/ \                                   ║
║                                        |.''._'--(o.o)--'_.''.|                                   ║
║                                         \_ / `;=/ ' \=;` \ _/                                    ║
║                                           `\__| \___/ |__/`                                      ║
║                                                \(_|_)/                                           ║
║                                                '' ` ''                                           ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
╚══════════════════════════════════════════════════════════════════════════════════════════════════╝", @"
╔══════════════════════════════════════════════════════════════════════════════════════════════════╗
║                                                                                                  ║
║                                                    .WWWW.                                        ║
║                                                   WWWWw`w`'                                      ║
║                                                 .WWWW O O                                        ║
║                                              .WWWW''WW.'-.                                       ║
║                                             WWWWWWWWWWWWW.                                       ║
║                                            WWWWWWWWWWWWWWW                                       ║
║                                            ''WWWWWWWWWW''\___                                    ║
║                                             /  /__ __/\___( \                                    ║
║                                            (____( \X(     /||\                                   ║
║                                               / /||\ \                                           ║
║                                               \______/                                           ║
║                                                \ | \ |                                           ║
║                                                 )|  \|                                           ║
║                                                (_|  /|                                           ║
║                                                |X| (X|                                           ║
║                                                |X| |X'._                                         ║
║                                               (__| (____)                                        ║
╚══════════════════════════════════════════════════════════════════════════════════════════════════╝", @"
╔══════════════════════════════════════════════════════════════════════════════════════════════════╗
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                __.-._                                            ║
║                                                '-._''7'                                          ║
║                                                 /'.-c                                            ║
║                                                 |  /T                                            ║
║                                                _)_/LI                                            ║
╚══════════════════════════════════════════════════════════════════════════════════════════════════╝", @"
╔══════════════════════════════════════════════════════════════════════════════════════════════════╗
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                   O                                              ║
║                                                  _|_                                             ║
║                                            ,_.-_' _ '_-._,                                       ║
║                                             \ (')(.)(') /                                        ║
║                                          _,  `\_-===-_/`  ,_                                     ║
║                                         >  |----''*''----|  <                                    ║
║                                         `**`--/   _-@-\--`**`                                    ║
║                                              |===L_I===|                                         ║
║                                               \       /                                          ║
║                                               _\__|__/_                                          ║
║                                              `wwww`wwww`                                         ║
╚══════════════════════════════════════════════════════════════════════════════════════════════════╝", @"
╔══════════════════════════════════════════════════════════════════════════════════════════════════╗
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                   //                                             ║
║                                                  (oo\                                            ║
║                                                  / ._)                                           ║
║                                                 J _=\=                                           ║
║                                                 |   /                                            ║
║                                            3._.' |_|_                                            ║
╚══════════════════════════════════════════════════════════════════════════════════════════════════╝", @"
╔══════════════════════════════════════════════════════════════════════════════════════════════════╗
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                                                                  ║
║                                                  &&&                                             ║
║                                                 (+.+)                                            ║
║                                               ___\=/___                                          ║
║                                              (|_ ~~~ _|)                                         ║
║                                                 |___|                                            ║
║                                                 / _ \                                            ║
║                                                /_/ \_\                                           ║
║                                               /_)   (_\                                          ║
╚══════════════════════════════════════════════════════════════════════════════════════════════════╝" };

            string promt = enemy[new Random().Next(0, enemy.Length)];

            string[] options = { "Attack", "Defend", "Run away" };
            StartMenu startMenu = new StartMenu(options);
            UserInterface ui = new UserInterface($"\n\n{promt}", "", startMenu);
            int selectedIndex = startMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    Attacked(ui);
                    break;
                case 1:
                    Defended(ui);
                    break;
                case 2:
                    RunAway(ui);
                    break;
            }

            Console.ReadKey(true);

        }

        private void Attacked(UserInterface ui)
        {
            ui.UpdateUi(ui.Map, "You attacked the enemy", ui.Menu);
            Console.ReadKey(true);
            Environment.Exit(0);
        }

        private void Defended(UserInterface ui)
        {
            ui.UpdateUi(ui.Map, "You used a shield", ui.Menu);
            Console.ReadKey(true);
            Environment.Exit(0);
        }
        
        private void RunAway(UserInterface ui)
        {
            ui.UpdateUi(ui.Map, "Press any key to run away", ui.Menu);
            Console.ReadKey(true);
            Environment.Exit(0);
        }
    }
}
