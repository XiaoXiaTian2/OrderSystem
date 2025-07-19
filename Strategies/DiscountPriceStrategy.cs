using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Strategies
{
    public class DiscountPriceStrategy : AStrategy
    {
        class ConditionItem
        {
            public string Name { get; set; }
            public int Number { get; set; }
            public int Price { get; set; }
            public int ConditionId { get; set; }
            public int TargetQty { get; set; }
            public ConditionItem(
                string name,
                int number,
                int price,
                int conditionId,
                int targetQty
            )
            {
                this.Name = name;
                this.Number = number;
                this.Price = price;
                this.ConditionId = conditionId;
                this.TargetQty = targetQty;
            }
        }

        public DiscountPriceStrategy(List<Item> items, MenuModel.Discount discount) : base(items, discount)
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

            List<ConditionItem> matchConditionItems = items.Select(item =>
            {
                var targetItem = conditionItems.FirstOrDefault(conditionItem => conditionItem.Name == item.name);
                if (targetItem == null)
                {
                    return null;
                }
                return new ConditionItem(targetItem.Name, int.Parse(item.amount), int.Parse(item.singlePrice), targetItem.ConditionID, targetItem.TargetQty);
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
                    TargetQty = condition.Key.TargetQty

                }).Where(x => x.Qty >= x.TargetQty).ToList();

            if (matchItemGroup.Count == 0)
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
                var avaliableItems = matchConditionItems
                    .GroupBy(item => item.ConditionId)
                    .Select(x =>
                    {
                        var t = x.SelectMany(y => Enumerable.Repeat(y.Price, y.Number).OrderBy(z => z));
                        return t.ToList();
                    }).ToList();

                int discountAmount;
                if (reward.Free_type == "fix")
                {
                    discountAmount = Enumerable.Range(0, leastFreeSets)
                        .Select(i =>
                        {
                            int total = avaliableItems.Sum(item => item[i]);
                            int setPrice = reward.Set_Price;
                            return setPrice - total > 0 ? 0 : setPrice - total;
                        })
                        .Sum(diff => diff);
                    //已購買item分組 => 遍歷隨機搭配 => 計算每一組的總額 => 取得折扣金額
                    //int oriTotalAmount = avaliableItems.Sum(item => int.Parse(item.singlePrice) * int.Parse(item.amount));
                    //int setPrice = reward.Set_Price * leastFreeSets;
                    //discountAmount = setPrice - oriTotalAmount > 0 ? 0 : setPrice - oriTotalAmount;
                }
                else
                {
                    discountAmount = leastFreeSets * reward.Discount_Price;
                }

                Item discountItem = new Item(
                "(折扣)" + reward.Name,
                reward.Discount_Price.ToString(),
                leastFreeSets.ToString(),
                discountAmount.ToString()
            );

                return discountItem;
            }).ToList();

            items.AddRange(discountItems);
        }
    }
}
