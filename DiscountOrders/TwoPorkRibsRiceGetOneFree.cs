using OrderSystem.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace OrderSystem.DiscountOrders
{
    class TwoPorkRibsRiceGetOneFree : BuyandFreeStrategy
    {
        public TwoPorkRibsRiceGetOneFree(List<Item> items, MenuModel.Discount discount) : base(items, discount)
        {
        }

        public override void DiscountOrder()
        {
            items.RemoveAll(x => x.name.Contains("贈送") || x.name.Contains("折扣"));

            Item food = items.FirstOrDefault(item => item.name == "排骨飯");
            if (food != null && int.Parse(food.amount) >= 2)
            {
                int fold = int.Parse(food.amount) / 2;
                Item newFood = new Item("(贈送)" + food.name, "0", fold.ToString(), "0");
                items.Add(newFood);
            }
        }
    }
}
