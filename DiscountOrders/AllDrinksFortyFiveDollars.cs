using OrderSystem.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.DiscountOrders
{
    class AllDrinksFortyFiveDollars : BuyandFreeStrategy
    {
        public AllDrinksFortyFiveDollars(List<Item> items, MenuModel.Discount discount) : base(items, discount)
        {
        }

        public override void DiscountOrder()
        {
            //if (drinks.Count > 0)
            //{
            //    int oriTotal = drinks.Sum(drink => int.Parse(drink.total));
            //    int count = drinks.Sum(drink => int.Parse(drink.amount));

            //    int newTotal = count * 45;

            //    int diff = oriTotal - newTotal;
            //    Item discount = new Item("(折扣)飲料均一價45元", "-" + diff.ToString(), "1", "-" + diff.ToString());
            //    items.Add(discount);
            //}
        }
    }
}
