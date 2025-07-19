using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Strategies
{
    class PercentConditionItem
    {
        public int ConditionId { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public int TargetQty { get; set; }
        public int Price { get; set; }
        public PercentConditionItem(
            int conditionId,
            string name,
            int number,
            int targetQty,
            int price
        )
        {
            this.ConditionId = conditionId;
            this.Name = name;
            this.Number = number;
            this.TargetQty = targetQty;
            this.Price = price;
        }
    }

    public class DiscountPercentStrategy : AStrategy
    {
        public DiscountPercentStrategy(List<Item> items, MenuModel.Discount discount) : base(items, discount)
        {
        }

        public override void DiscountOrder()
        {
            // 取得條件
            var conditionItems = discount.Conditions.SelectMany((condition, ID) =>
            {
                return condition.Name.Split('|')
                .Select(name => new
                {
                    Name = name,
                    ConditionID = ID,
                    TargetQty = condition.Number
                });
            }).ToList();

            List<PercentConditionItem> matchConditionItems = items.Select(item =>
            {
                var targetItem = conditionItems.FirstOrDefault(conditionItem => conditionItem.Name == item.name);
                if (targetItem == null)
                {
                    return null;
                }
                return new PercentConditionItem(targetItem.ConditionID, targetItem.Name, int.Parse(item.amount), targetItem.TargetQty, int.Parse(item.singlePrice));
            }).Where(item => item != null).ToList();

            if (matchConditionItems.GroupBy(x => x.ConditionId).ToList().Count < discount.Conditions.Length)
            {
                return;
            }

            // 分組
            var matchItemGroup = matchConditionItems
                .GroupBy(x => new { x.ConditionId, x.TargetQty })
                .Select(condition => new
                {
                    ConditionID = condition.Key.ConditionId,
                    Qty = condition.Sum(x => x.Number),
                    Price = condition.Sum(x => x.Price),
                    TargetQty = condition.Key.TargetQty,

                }).Where(x => x.Qty >= x.TargetQty).ToList();

            if (matchItemGroup.Count != discount.Conditions.Length)
            {
                return;
            }

            int leastFreeSets = matchItemGroup.Select(item => new
            {
                fold = item.Qty / item.TargetQty
            }
            ).Min(x => x.fold);


            List<Item> discountItems = discount.Rewards.Select(reward =>
            {
                var totalDiscountAmount = (int)matchConditionItems
                    .GroupBy(item => new { item.ConditionId, item.TargetQty })
                    .Select(x =>
                    {
                        return x.SelectMany(y => Enumerable.Repeat(y.Price, y.Number).OrderBy(z => z)).Take(x.Key.TargetQty).Sum();
                    }).Sum(total =>
                    {
                        float discountPrice = (reward.Discount_Percentage * total);
                        return ((discountPrice * 100 - total * 100) / 100) * leastFreeSets;
                    });

                Item discountItem = new Item(
                "(折扣)" + reward.Name,
                (totalDiscountAmount / leastFreeSets).ToString(),
                leastFreeSets.ToString(),
                totalDiscountAmount.ToString()
            );

                return discountItem;
            }).ToList();

            items.AddRange(discountItems);
        }
    }
}
