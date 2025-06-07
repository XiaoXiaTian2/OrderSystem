using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.DiscountOrders
{
    class PorkRibsRiceThreeMinusTenDollar : Discount
    {
        public override void CalDisount(List<Item> items)
        {
            items.RemoveAll(x => x.name.Contains("贈送") || x.name.Contains("折扣"));

            Item food = items.FirstOrDefault(item => item.name == "排骨飯");
            if (food != null && int.Parse(food.amount) >= 3)
            {
                int discounts = (int.Parse(food.amount) / 3);
                int discountPrice = discounts * 10;
                Item discount = new Item("(折扣)" + food.name, "-10", discounts.ToString(), "-" + discountPrice.ToString());
                items.Add(discount);
            }
        }
    }
}
