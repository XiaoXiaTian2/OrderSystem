using OrderSystem.Strategies;
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

        public static void AddOrder(MenuModel.Discount discount, Item item)
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
            //Type symbolType = Type.GetType(discountType);
            //AStrategy strategy = (AStrategy)Activator.CreateInstance(symbolType);

            //Discount.DiscountOrder(strategy, _orderDetails);
            ChangeDiscountType(discount);
        }

        public static void ChangeDiscountType(MenuModel.Discount discount)
        {
            Discount.DiscountOrder(discount, _orderDetails);
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
