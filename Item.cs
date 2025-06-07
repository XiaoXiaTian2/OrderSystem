using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem
{
    public class Item
    {
        public string name { get; set; }
        public string singlePrice { get; set; }
        public string amount { get; set; }
        public string total { get; set; }


        public Item(
            string name,
            string singlePrice,
            string amount,
            string total
        )
        {
            this.name = name;
            this.singlePrice = singlePrice;
            this.amount = amount;
            this.total = total;
        }
    }
}
