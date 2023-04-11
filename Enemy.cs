using RogueFefu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace RogueFefu
{
    internal class Enemy
    {
        private static Random rand = new Random();
        private const int STARTING_HP = 25;
        private const int STARTING_STRENGTH = 2;

        public int HP { get; set; }
        public int HPDamage { get; set; }
        public int Strength { get; set; }
        public MapSpace? Location { get; set; }

        public Enemy()
        {
            var rand = new Random();
            this.HP = STARTING_HP;
            this.Strength = STARTING_STRENGTH;
        }
    }
}