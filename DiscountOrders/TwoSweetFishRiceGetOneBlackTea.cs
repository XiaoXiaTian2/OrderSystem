using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.DiscountOrders
{
    class TwoSweetFishRiceGetOneBlackTea : Discount
    {
        public override void CalDisount(List<Item> items)
        {
            items.RemoveAll(x => x.name.Contains("贈送") || x.name.Contains("折扣"));

            Item rice = items.FirstOrDefault(item => item.name == "香魚飯");

            if (rice != null && int.Parse(rice.amount) >= 2)
            {
                int times = int.Parse(rice.amount) / 2;

                Item discount = new Item("(贈送)紅茶", "0", times.ToString(), "0");
                items.Add(discount);
            }
        }
    }
}
