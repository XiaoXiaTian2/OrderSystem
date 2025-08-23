using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.AI.Tools.DiscountSelector.Request
{
    internal class DiscountSelectorProperties
    {
        public Args name { get; set; }
        public Args strategy { get; set; }
        public Args reason { get; set; }
    }
}
