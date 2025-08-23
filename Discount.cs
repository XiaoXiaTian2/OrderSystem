using OrderSystem.Models;
using OrderSystem.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OrderSystem.AI.Common.AIRequestModel;

namespace OrderSystem
{
    public class Discount
    {
        public virtual void CalDisount(List<Item> items)
        {
        }

        //public static void DiscountOrder(MenuModel.Discount discount, List<Item> items)
        public static async Task DiscountOrder(OrderRequest orderRequest)
        {
            Console.WriteLine("before" + String.Join(",", orderRequest.OrderList.Select(x => x.name)));

            orderRequest.OrderList.RemoveAll(x => x.name.Contains("贈送") || x.name.Contains("折扣"));
            Console.WriteLine("after" + String.Join(",", orderRequest.OrderList.Select(x => x.name)));

            DiscountContext discountContext = new DiscountContext(orderRequest);
            var discountResult = await discountContext.HandleDiscount();

            //Discount discount = DiscountOrderFactory.CreateDiscountOrder(orderType);
            //orderType.CalDisount(items);
            ShowPanel.GeneratePanel(orderRequest.OrderList, discountResult);

        }
    }
}
