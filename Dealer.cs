using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueFefu
{
    internal class Dealer
    {
        public const char CHARACTER = 'D';

        public MapSpace? Location { get; set; }

        public Dealer()
        {
            var rand = new Random();
        }
    }
}
