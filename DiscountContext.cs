using OrderSystem.AI.Common;
using OrderSystem.AI.Constant;
using OrderSystem.AI.Tools.DiscountSelector.Response;
using OrderSystem.Models;
using OrderSystem.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OrderSystem.AI.Common.AIRequestModel;
using static OrderSystem.MenuModel;

namespace OrderSystem
{
    internal class DiscountContext
    {
        private OrderRequest orderRequest;

        public DiscountContext(OrderRequest orderRequest)
        {
            this.orderRequest = orderRequest;
        }

        public async Task<(string, string)> HandleDiscount()
        {
            if (orderRequest.AIRecommand == false)
            {
                Type discountType = Type.GetType(orderRequest.DiscountType.Strategy);
                AStrategy strategy = (AStrategy)Activator.CreateInstance(discountType, new object[] { orderRequest.OrderList, orderRequest.DiscountType });
                strategy.DiscountOrder();

                return ("abc", "");
            }

            AIAgent aIAgent = new AIAgent();
            aIAgent.AddPrompt(Role.Model, $"你需要根據下列菜單和折扣的JSON內容為使用者選擇最合適的折扣方案: {MenuData.MenuJSON}"); // 送菜單

            string itemString = String.Join(", ", this.orderRequest.OrderList.Select(item => $"{item.name}:{item.amount}份"));
            aIAgent.AddPrompt(Role.User, $"這是我選取的菜單內容: {itemString}，幫我篩選出最優惠的折扣方案");

            var aIResult = await aIAgent.ExecRequest();
            if (aIResult != null && aIResult.CanUseTool == true)
            {
                Console.WriteLine("use tool");
                DiscountSelectorResponseArgs result = (DiscountSelectorResponseArgs)aIResult.UseTool();
                string strategyClass = result.strategy;
                MenuModel.Discount discount = MenuData.Discounts.First(dis => dis.Name == result.name);

                Type discountType = Type.GetType(strategyClass);
                AStrategy strategy = (AStrategy)Activator.CreateInstance(discountType, new object[] { orderRequest.OrderList, discount });
                strategy.DiscountOrder();
                //OrderSystem.Discount.DiscountOrder(orderRequest);

                return (result.reason, result.name);
            }
            return ("", "");
        }
    }
}
