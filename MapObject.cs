﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RogueFefu
{
    public class MapObject
    {
        public int hp;
        public int damage;
        public MapObject(int hp,int damage)
        {
            this.hp = hp;
            this.damage = damage;
        }
        public virtual void Print()
        {
            Console.WriteLine(this.hp);
        }
    }
    
}
