using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.DiscountOrders
{
    class AllDrinksFortyFiveDollars : Discount
    {
        public override void CalDisount(List<Item> items)
        {
            items.RemoveAll(x => x.name.Contains("贈送") || x.name.Contains("折扣"));

            List<Item> drinks = items.Where(item => item.name == "紅茶" || item.name == "綠茶" || item.name == "奶茶" || item.name == "水果茶").ToList();

            if (drinks.Count > 0)
            {
                int oriTotal = drinks.Sum(drink => int.Parse(drink.total));
                int count = drinks.Sum(drink => int.Parse(drink.amount));

                int newTotal = count * 45;

                int diff = oriTotal - newTotal;
                Item discount = new Item("(折扣)飲料均一價45元", "-" + diff.ToString(), "1", "-" + diff.ToString());
                items.Add(discount);
            }
        }
    }
}
