using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Strategies
{
    public abstract class AStrategy
    {
        protected List<Item> items { get; set; }
        protected MenuModel.Discount discount { get; set; }
        public AStrategy(List<Item> items, MenuModel.Discount discount)
        {
            this.items = items;
            this.discount = discount;
        }

        public abstract void DiscountOrder();
    }
}
