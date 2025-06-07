using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.DiscountOrders
{
    class SweetFishRiceWithSeaweedSoupGetTwentyPercentOff : Discount
    {
        public override void CalDisount(List<Item> items)
        {
            items.RemoveAll(x => x.name.Contains("贈送") || x.name.Contains("折扣"));

            Item rice = items.FirstOrDefault(item => item.name == "香魚飯");
            Item soup = items.FirstOrDefault(item => item.name == "海帶湯");

            if (rice != null && soup != null)
            {
                int times = Math.Min(int.Parse(rice.amount), int.Parse(soup.amount));
                double decreasePrice = (int.Parse(rice.singlePrice) + int.Parse(soup.singlePrice)) * 0.2;
                int singlePrice = (int)decreasePrice;
                int total = singlePrice * times;

                Item discount = new Item("(折扣)香魚飯搭配海帶湯打八折", "-" + singlePrice.ToString(), times.ToString(), total.ToString());
                items.Add(discount);
            }
        }
    }
}
