using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.DiscountOrders
{
    class ChickenChopRiceWithMilkTeaGetOneSouffle : Discount
    {
        public override void CalDisount(List<Item> items)
        {
            items.RemoveAll(x => x.name.Contains("贈送") || x.name.Contains("折扣"));

            Item rice = items.FirstOrDefault(item => item.name == "雞排飯");
            Item drink = items.FirstOrDefault(item => item.name == "奶茶");

            if (rice != null && drink != null)
            {
                int times = Math.Min(int.Parse(rice.amount), int.Parse(drink.amount));

                Item discount = new Item("(贈送)舒芙蕾", "0", times.ToString(), "0");
                items.Add(discount);
            }
        }
    }
}
