using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueFefu
{
    internal class Player
    {
        private const int STARTING_HP = 12;
        private const int STARTING_STRENGTH = 16;
        public const char CHARACTER = '@';//☺

        

        public string PlayerName { get; set; }
        public int HP { get; set; }
        public int HPDamage { get; set; }
        public int Strength { get; set; }
        public int Gold { get; set; }
        public int Experience { get; set; }
        
        public int HungerTurn { get; set; }
        public bool HasAmulet { get; set; }
        public MapSpace? Location { get; set; }

        public Player(string PlayerName)
        {
            var rand = new Random();
            this.PlayerName = PlayerName;
            this.HP = STARTING_HP;
            this.HPDamage = 0;
            this.Strength = STARTING_STRENGTH;
            this.Gold = 0;
            this.Experience = 1;
        }
    }
}
