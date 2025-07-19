using OrderSystem.DiscountOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem
{
    class DiscountOrderFactory
    {
        public static Discount CreateDiscountOrder(string discountString)
        {
            Discount discount = null;
            //switch (discountString)
            //{
            //    case "雞腿飯買二送一":
            //        discount = new TwoPorkRibsRiceGetOneFree();
            //        break;
            //    case "排骨飯買三個350元":
            //        discount = new PorkRibsRiceThreeMinusTenDollar();
            //        break;
            //    case "雞排飯加餛飩湯190元":
            //        discount = new ChickenChopRiceWithWontonSoup();
            //        break;
            //    case "香魚飯買兩個送一杯紅茶":
            //        discount = new TwoSweetFishRiceGetOneBlackTea();
            //        break;
            //    case "香魚飯搭配海帶湯打八折":
            //        discount = new SweetFishRiceWithSeaweedSoupGetTwentyPercentOff();
            //        break;
            //    case "雞排飯加奶茶送一盤舒芙蕾":
            //        discount = new ChickenChopRiceWithMilkTeaGetOneSouffle();
            //        break;
            //    case "排骨飯三個打85折":
            //        discount = new ThreePorkRibsRiceGetFiftyFivePercentOff();
            //        break;
            //    case "飲料均一價45元":
            //        discount = new AllDrinksFortyFiveDollars();
            //        break;
            //    case "飲料任買三杯送一杯(送最便宜的)":
            //        discount = new ThreeDrinksGetCheapestOneFree();
            //        break;
            //    case "全場消費滿350打八折":
            //        discount = new ConsumptionOver350GetTwentyPercentOff();
            //        break;
            //    case "全場消費一律95折":
            //        discount = new AllFivePercentOff();
            //        break;
            //}
            return discount;
        }
    }
}
