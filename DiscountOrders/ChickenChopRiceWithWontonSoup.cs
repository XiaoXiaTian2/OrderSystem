using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.DiscountOrders
{
    class ChickenChopRiceWithWontonSoup : Discount
    {
        public override void CalDisount(List<Item> items)
        {
            items.RemoveAll(x => x.name.Contains("贈送") || x.name.Contains("折扣"));

            Item chicken = items.FirstOrDefault(item => item.name == "雞排飯");
            Item soup = items.FirstOrDefault(item => item.name == "餛飩湯");

            if (chicken != null && soup != null)
            {
                int times = Math.Min(int.Parse(chicken.amount), int.Parse(soup.amount));
                int total = 5 * times;

                Item discount = new Item("(折扣)雞排飯加餛飩湯190元", "-5", times.ToString(), "-" + total.ToString());
                items.Add(discount);
            }
        }
    }
}
