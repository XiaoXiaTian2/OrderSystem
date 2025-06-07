using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.DiscountOrders
{
    class ThreeDrinksGetCheapestOneFree : Discount
    {
        public override void CalDisount(List<Item> items)
        {
            items.RemoveAll(x => x.name.Contains("贈送") || x.name.Contains("折扣"));

            List<Item> drinks = items.Where(item => item.name == "紅茶" || item.name == "綠茶" || item.name == "奶茶" || item.name == "水果茶").ToList();

            if (drinks.Count > 0)
            {
                int count = drinks.Sum(drink => int.Parse(drink.amount));

                int bonus = count / 3;
                if (bonus > 0)
                {
                    Item discount = new Item("(贈送)紅茶飲料任買三杯送一杯", "0", bonus.ToString(), "0");
                    items.Add(discount);
                }
            }
        }
    }
}
