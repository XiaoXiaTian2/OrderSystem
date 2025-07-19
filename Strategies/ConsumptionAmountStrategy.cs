using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OrderSystem.MenuModel;

namespace OrderSystem.Strategies
{
    public class ConsumptionAmountStrategy : AStrategy
    {
        class ConsumptionConditionItem
        {
            public int ConditionId { get; set; }
            public string Name { get; set; }
            public int Price { get; set; }
            public int Qty { get; set; }
            public int TargerPrice { get; set; }
            public ConsumptionConditionItem(
                int conditionId,
                string name,
                int price,
                int Qty,
                int TargerPrice
            )
            {
                this.ConditionId = conditionId;
                this.Name = name;
                this.Price = price;
                this.Qty = Qty;
                this.TargerPrice = TargerPrice;
            }
        }
        public ConsumptionAmountStrategy(List<Item> items, MenuModel.Discount discount) : base(items, discount)
        {
        }

        public override void DiscountOrder()
        {
            int leastFreeSets = 1;
            List<Item> discountItems = null;

            var conditionItems = discount.Conditions.SelectMany((condition, ID) =>
            {
                return condition.Name.Split('|')
                .Select(name => new
                {
                    ConditionID = ID,
                    Name = name,
                    TargetPrice = condition.Total_Price
                });
            }).ToList();

            bool isAll = false;

            int allOriginAmount = items.Sum(item => int.Parse(item.total));
            List<ConsumptionConditionItem> matchConditionItems = items.Select(item =>
            {
                var targetItem = conditionItems.FirstOrDefault(conditionItem =>
                {
                    if (conditionItem.Name == "" && allOriginAmount >= conditionItem.TargetPrice)
                    {
                        isAll = true;
                        return false;
                    }
                    return conditionItem.Name == item.name;
                });
                if (targetItem == null)
                {
                    return null;
                }
                return new ConsumptionConditionItem(targetItem.ConditionID, targetItem.Name, int.Parse(item.singlePrice), int.Parse(item.amount), targetItem.TargetPrice);
            }).Where(item => item != null).ToList();

            if (!isAll)
            {
                var matchItemGroup = matchConditionItems
                    .GroupBy(x => new { x.ConditionId, x.TargerPrice })
                    .Select(condition => new
                    {
                        ConditionID = condition.Key.ConditionId,
                        Qty = condition.Sum(x => x.Qty),
                        Price = condition.Sum(x => x.Price),
                        TargetPrice = condition.Key.TargerPrice

                    }).Where(x => x.Qty * x.Price >= x.TargetPrice).ToList();

                if (matchItemGroup.Count != discount.Conditions.Length)
                {
                    return;
                }

                if (matchConditionItems.GroupBy(x => x.ConditionId).ToList().Count < discount.Conditions.Length)
                {
                    return;
                }


                discountItems = discount.Rewards.Select(reward =>
                {
                    var totalDiscountAmount = (int)matchConditionItems
                        .GroupBy(item => new { item.ConditionId, item.Name })
                        .Select(x =>
                        {
                            return x.SelectMany(y => Enumerable.Repeat(y.Price, y.Qty)).Sum();
                        }).Sum(total =>
                        {
                            float discountPrice = (reward.Discount_Percentage * total);
                            return ((discountPrice * 100 - total * 100) / 100) * leastFreeSets;
                        });

                    Item discountItem = new Item(
                        "(折扣)" + reward.Name,
                        totalDiscountAmount.ToString(),
                        leastFreeSets.ToString(),
                        totalDiscountAmount.ToString()
                    );

                    return discountItem;
                }).ToList();

            }
            else
            {
                discountItems = discount.Rewards.Select(reward =>
                {
                    var totalDiscountAmount = (int)items.Sum(item =>
                    {
                        float discountPrice = (reward.Discount_Percentage * int.Parse(item.total));
                        return ((discountPrice * 100 - int.Parse(item.total) * 100) / 100) * leastFreeSets;
                    });

                    Item discountItem = new Item(
                    "(折扣)" + reward.Name,
                    totalDiscountAmount.ToString(),
                    leastFreeSets.ToString(),
                    totalDiscountAmount.ToString()
                );
                    return discountItem;

                }).ToList();

            }

            items.AddRange(discountItems);
        }
    }
}
