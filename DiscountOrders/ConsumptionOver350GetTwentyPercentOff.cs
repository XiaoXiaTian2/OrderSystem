using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.DiscountOrders
{
    class ConsumptionOver350GetTwentyPercentOff : Discount
    {
        public override void CalDisount(List<Item> items)
        {
            items.RemoveAll(x => x.name.Contains("贈送") || x.name.Contains("折扣"));

            int totalPrice = items.Sum(item => int.Parse(item.total));

            if (totalPrice >= 350)
            {
                double substract = (totalPrice * 0.2);
                int sub = (int)substract;

                Item discount = new Item("(折扣)全場消費滿350打八折", sub.ToString(), "1", "-" + sub.ToString());
                items.Add(discount);
            }
        }
    }
}
