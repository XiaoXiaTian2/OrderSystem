using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.DiscountOrders
{
    class ThreePorkRibsRiceGetFiftyFivePercentOff : Discount
    {
        public override void CalDisount(List<Item> items)
        {
            items.RemoveAll(x => x.name.Contains("贈送") || x.name.Contains("折扣"));

            Item rice = items.FirstOrDefault(item => item.name == "排骨飯");

            if (rice != null && int.Parse(rice.amount) >= 3)
            {
                int times = int.Parse(rice.amount) / 3;
                double decreasePrice = int.Parse(rice.singlePrice) * 0.2;
                int singlePrice = (int)decreasePrice;
                int total = singlePrice * times;

                Item discount = new Item("(折扣)排骨飯三個打85折", "-" + singlePrice.ToString(), times.ToString(), "-" + total.ToString());
                items.Add(discount);
            }
        }

    }
}
