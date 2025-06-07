using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderSystem
{
    class Order
    {
        private static List<Item> _orderDetails = new List<Item>();

        public static void AddOrder(string discountType, Item item)
        {
            Item orderItem = _orderDetails.FirstOrDefault(order => order.name == item.name);
            if (orderItem == null)
            {
                _orderDetails.Add(item);
            }
            else
            {
                if (item.amount == "0")
                {
                    _orderDetails.Remove(orderItem);
                }
                else
                {
                    orderItem.amount = item.amount;
                    orderItem.total = item.total;
                }
            }
            //Discount discount = DiscountOrderFactory.CreateDiscountOrder(discountType);
            //discount.CalDisount(_orderDetails);
            Discount.DiscountOrder(discountType, _orderDetails);

        }

        public static void ChangeDiscountType(string discountType)
        {
            Discount.DiscountOrder(discountType, _orderDetails);

        }


        public static string GetPriceTotal()
        {
            int allTotal = 0;
            foreach (var item in _orderDetails)
            {
                string total = item.total;
                allTotal += int.Parse(total);
            }

            return allTotal.ToString();
        }
    }
}
