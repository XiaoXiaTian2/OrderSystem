using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.DiscountOrders
{
    class ThreeChickenChopRiceAndTwoBubbleTeaGetOneSouffle : Discount
    {
        public override void CalDisount(List<Item> items)
        {
            Item rice = items.FirstOrDefault(item => item.name == "雞排飯");
            Item drink = items.FirstOrDefault(item => item.name == "珍奶");

            if (rice != null && int.Parse(rice.amount) >= 3
                && drink != null && int.Parse(drink.amount) >= 2
            )
            {
                int riceSets = int.Parse(rice.amount) / 3;
                int drinkSets = int.Parse(drink.amount) / 2;
                int giftSets = Math.Min(riceSets, drinkSets);

                Item discount = new Item("(贈送)舒芙蕾", "0", giftSets.ToString(), "0");
                items.Add(discount);
            }
        }

    }
}
