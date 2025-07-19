using OrderSystem.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem
{
    public class Discount
    {
        public virtual void CalDisount(List<Item> items)
        {
        }

        public static void DiscountOrder(MenuModel.Discount discount, List<Item> items)
        {
            //雞腿飯買二送一
            //排骨飯買三個350元
            //雞排飯加餛飩湯190元
            //香魚飯買兩個送一杯紅茶
            //香魚飯搭配海帶湯打八折
            //雞排飯加奶茶送一盤舒芙蕾
            //排骨飯三個打85折
            //飲料均一價45元
            //飲料任買三杯送一杯(送最便宜的)
            //全場消費滿350打八折
            //全場消費一律95折

            items.RemoveAll(x => x.name.Contains("贈送") || x.name.Contains("折扣"));

            Type discountType = Type.GetType(discount.Strategy);
            AStrategy strategy = (AStrategy)Activator.CreateInstance(discountType, new object[] { items, discount });

            strategy.DiscountOrder();
            //Discount discount = DiscountOrderFactory.CreateDiscountOrder(orderType);
            //orderType.CalDisount(items);
            ShowPanel.GeneratePanel(items);

        }
    }
}
