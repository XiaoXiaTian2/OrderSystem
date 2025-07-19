using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.DiscountOrders
{
    class DesertOver799GetTwentyFivePercentOff : Discount
    {
        public override void CalDisount(List<Item> items)
        {
            int totalPrice = items.Sum(item => int.Parse(item.total));

            double substract = (totalPrice * 0.05);
            int sub = (int)substract;

            Item discount = new Item("(折扣)甜點滿799打75折", sub.ToString(), "1", "-" + sub.ToString());
            items.Add(discount);
        }
    }
}
