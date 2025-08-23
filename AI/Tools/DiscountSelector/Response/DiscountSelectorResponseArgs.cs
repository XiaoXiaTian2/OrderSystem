using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.AI.Tools.DiscountSelector.Response
{
    internal class DiscountSelectorResponseArgs
    {
        public string name { get; set; }
        public string strategy { get; set; }
        public string reason { get; set; }
    }
}
