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

        public int HP { get; set; }
        public int HPDamage { get; set; }
        public int Strength { get; set; }
        public MapSpace? Location { get; set; }

        public Enemy()
        {
            var rand = new Random();
            this.HP = 100;
            this.HPDamage = 0;
            this.Strength = 100;
        }
    }
}