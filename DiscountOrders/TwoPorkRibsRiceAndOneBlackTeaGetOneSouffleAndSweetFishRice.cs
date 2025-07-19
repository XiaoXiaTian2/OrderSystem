using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.DiscountOrders
{
    class TwoPorkRibsRiceAndOneBlackTeaGetOneSouffleAndSweetFishRice : Discount
    {
        public override void CalDisount(List<Item> items)
        {
            Item rice = items.FirstOrDefault(item => item.name == "排骨飯");
            Item drink = items.FirstOrDefault(item => item.name == "紅茶");

            if (rice != null && int.Parse(rice.amount) >= 2
                && drink != null && int.Parse(drink.amount) >= 1
            )
            {
                int riceSets = int.Parse(rice.amount) / 2;
                int drinkSets = int.Parse(drink.amount);
                int giftSets = Math.Min(riceSets, drinkSets);

                Item discount = new Item("(贈送)舒芙蕾及紅茶", "0", giftSets.ToString(), "0");
                items.Add(discount);
            }
        }

    }
}
