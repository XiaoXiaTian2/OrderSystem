using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static OrderSystem.MenuModel;

namespace OrderSystem.Strategies
{
    class ConditionItem
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public int ConditionId { get; set; }
        public int TargetQty { get; set; }
        public ConditionItem(
            string name,
            int number,
            int conditionId,
            int targetQty
        )
        {
            this.Name = name;
            this.Number = number;
            this.ConditionId = conditionId;
            this.TargetQty = targetQty;
        }
    }
    class RewardItem
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public int ConditionId { get; set; }
        public int TargetQty { get; set; }
        public RewardItem(
            string name,
            int number,
            int conditionId,
            int targetQty
        )
        {
            this.Name = name;
            this.Number = number;
            this.ConditionId = conditionId;
            this.TargetQty = targetQty;
        }
    }


    public class BuyandFreeStrategy : AStrategy
    {
        public BuyandFreeStrategy(List<Item> items, MenuModel.Discount discount) : base(items, discount)
        {
        }

        public override void DiscountOrder()
        {
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
                return new ConditionItem(targetItem.Name, int.Parse(item.amount), targetItem.ConditionID, targetItem.TargetQty);
            }).Where(item => item != null).ToList();

            if (matchConditionItems.Count == 0)
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
                    TargetQty = condition.Key.TargetQty

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

            // FIXME
            //var rewardItems = discount.Rewards.SelectMany((reward, ID) =>
            //{
            //    return reward.Name.Split('|')
            //    .Select(name => new
            //    {
            //        Name = name,
            //        ConditionID = ID,
            //        Qty = reward.Number,
            //        FreeType = reward.Free_type
            //    });
            //}).Where(x=> items.FirstOrDefault(y=>y.name == x.Name) != null)
            //.GroupBy(z => new
            //{
            //    ConditionID = z.ConditionID,
            //    Qty = z.Qty,
            //    FreeType = z.FreeType
            //}).Select(a =>
            //{
            //    string freeItem = getFreeItem(a.Key.ToList(), a.Key.FreeType);
            //    int number = leastFreeSets * a.Key.Qty;
            //    Item newFood = new Item("(贈送)" + freeItem, "0", number.ToString(), "0");

            //    return newFood;
            //}).ToList();


            //items.Where(item => rewardItems.FirstOrDefault(reward => reward.Name == item.name)!=null)
            //    .GroupBy(x => new
            //     {

            //     })
            //    .Select(rewardGroup =>
            //    {
            //        string freeItem = getFreeItem(rewardGroup.ToList(), rewardGroup.Key.FreeType);
            //        int number = leastFreeSets * reward.Number;
            //        Item newFood = new Item("(贈送)" + freeItem, "0", number.ToString(), "0");

            //        return newFood;

            //    });



            //var rewardItemGroup = rewardItems.GroupBy(x => x.ConditionId)
            //    .Select(rewardGroup => new
            //    {
            //        ConditionID = rewardGroup.Key.ConditionId,
            //        Qty = condition.Sum(x => x.Number),
            //        TargetQty = condition.Key.TargetQty

            //    }).Where(x => x.Qty >= x.TargetQty).ToList();
            List<Item> giftItems = discount.Rewards.Select(reward =>
            {
                // Rewards攤平 => 分群取差異(avaliableItems) => 按照freetype取得唯一item
                // 從贈送及購買品項中比對可贈送的品項
                List<Item> avaliableItems = new List<Item>();
                if (reward.Name.Contains('|'))
                {
                    avaliableItems = items
                       .Where(item => reward.Name.Contains(item.name)).ToList();
                }
                else
                {
                    Item add = new Item(reward.Name, "0", "0", "0");
                    avaliableItems.Add(add);
                }
                if (avaliableItems.Count == 0) return null;
                string freeItem = getFreeItem(avaliableItems, reward.Free_type);
                int number = leastFreeSets * reward.Number;
                Item newFood = new Item("(贈送)" + freeItem, "0", number.ToString(), "0");

                return newFood;
            }).Where(item => item != null).ToList();
            items.AddRange(giftItems);
        }

        private string getFreeItem(List<Item> availableItems, string freeType)
        {
            string freeItem;
            switch (freeType)
            {
                case "min":
                    freeItem = availableItems.First(item => int.Parse(item.singlePrice) == availableItems.Min(x => int.Parse(x.singlePrice))).name;
                    break;
                case "max":
                    freeItem = availableItems.First(item => int.Parse(item.singlePrice) == availableItems.Max(x => int.Parse(x.singlePrice))).name;
                    break;
                case "random":
                    Random rnd = new Random();
                    freeItem = availableItems[rnd.Next(0, availableItems.Count)].name;
                    break;
                default:
                    freeItem = availableItems[0].name;
                    break;
            }
            return freeItem;
        }
    }
}
