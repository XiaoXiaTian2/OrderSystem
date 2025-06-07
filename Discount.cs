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

        public static void DiscountOrder(string orderType, List<Item> items)
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

            //items.RemoveAll(x => x.name.Contains("贈送") || x.name.Contains("折扣"));

            Discount discount = DiscountOrderFactory.CreateDiscountOrder(orderType);
            discount.CalDisount(items);
            //if (orderType == "雞腿飯買二送一")
            //{
            //    Item food = items.FirstOrDefault(item => item.name == "雞腿飯");
            //    if (food != null && int.Parse(food.amount) >= 2)
            //    {
            //        int fold = int.Parse(food.amount) / 2;
            //        Item newFood = new Item("(贈送)" + food.name, "0", fold.ToString(), "0");
            //        items.Add(newFood);
            //    }

            //}

            //if (orderType == "排骨飯買三個350元")
            //{
            //    Item food = items.FirstOrDefault(item => item.name == "排骨飯");
            //    if (food != null && int.Parse(food.amount) >= 3)
            //    {
            //        int discounts = (int.Parse(food.amount) / 3);
            //        int discountPrice = discounts * 10;
            //        Item discount = new Item("(折扣)" + food.name, "-10", discounts.ToString(), "-" + discountPrice.ToString());
            //        items.Add(discount);
            //    }
            //}

            //if (orderType == "雞排飯加餛飩湯190元")
            //{
            //    Item chicken = items.FirstOrDefault(item => item.name == "雞排飯");
            //    Item soup = items.FirstOrDefault(item => item.name == "餛飩湯");

            //    if (chicken != null && soup != null)
            //    {
            //        int times = Math.Min(int.Parse(chicken.amount), int.Parse(soup.amount));
            //        int total = 5 * times;

            //        Item discount = new Item("(折扣)雞排飯加餛飩湯190元", "-5", times.ToString(), "-" + total.ToString());
            //        items.Add(discount);
            //    }
            //}

            //if (orderType == "香魚飯買兩個送一杯紅茶")
            //{
            //    Item rice = items.FirstOrDefault(item => item.name == "香魚飯");

            //    if (rice != null && int.Parse(rice.amount) >= 2)
            //    {
            //        int times = int.Parse(rice.amount) / 2;

            //        Item discount = new Item("(贈送)紅茶", "0", times.ToString(), "0");
            //        items.Add(discount);
            //    }
            //}

            //if (orderType == "香魚飯搭配海帶湯打八折")
            //{
            //    Item rice = items.FirstOrDefault(item => item.name == "香魚飯");
            //    Item soup = items.FirstOrDefault(item => item.name == "海帶湯");

            //    if (rice != null && soup != null)
            //    {
            //        int times = Math.Min(int.Parse(rice.amount), int.Parse(soup.amount));
            //        double decreasePrice = (int.Parse(rice.singlePrice) + int.Parse(soup.singlePrice)) * 0.2;
            //        int singlePrice = (int)decreasePrice;
            //        int total = singlePrice * times;

            //        Item discount = new Item("(折扣)香魚飯搭配海帶湯打八折", "-" + singlePrice.ToString(), times.ToString(), total.ToString());
            //        items.Add(discount);
            //    }
            //}

            //if (orderType == "雞排飯加奶茶送一盤舒芙蕾")
            //{
            //    Item rice = items.FirstOrDefault(item => item.name == "雞排飯");
            //    Item drink = items.FirstOrDefault(item => item.name == "奶茶");

            //    if (rice != null && drink != null)
            //    {
            //        int times = Math.Min(int.Parse(rice.amount), int.Parse(drink.amount));

            //        Item discount = new Item("(贈送)舒芙蕾", "0", times.ToString(), "0");
            //        items.Add(discount);
            //    }
            //}

            //if (orderType == "排骨飯三個打85折")
            //{
            //    Item rice = items.FirstOrDefault(item => item.name == "排骨飯");

            //    if (rice != null && int.Parse(rice.amount) >= 3)
            //    {
            //        int times = int.Parse(rice.amount) / 3;
            //        double decreasePrice = int.Parse(rice.singlePrice) * 0.2;
            //        int singlePrice = (int)decreasePrice;
            //        int total = singlePrice * times;

            //        Item discount = new Item("(折扣)排骨飯三個打85折", "-" + singlePrice.ToString(), times.ToString(), "-" + total.ToString());
            //        items.Add(discount);
            //    }
            //}

            //if (orderType == "飲料均一價45元")
            //{
            //    List<Item> drinks = items.Where(item => item.name == "紅茶" || item.name == "綠茶" || item.name == "奶茶" || item.name == "水果茶").ToList();

            //    if (drinks.Count > 0)
            //    {
            //        int oriTotal = drinks.Sum(drink => int.Parse(drink.total));
            //        int count = drinks.Sum(drink => int.Parse(drink.amount));

            //        int newTotal = count * 45;

            //        int diff = oriTotal - newTotal;
            //        Item discount = new Item("(折扣)飲料均一價45元", "-" + diff.ToString(), "1", "-" + diff.ToString());
            //        items.Add(discount);
            //    }
            //}

            //if (orderType == "飲料任買三杯送一杯(送最便宜的)")
            //{
            //    List<Item> drinks = items.Where(item => item.name == "紅茶" || item.name == "綠茶" || item.name == "奶茶" || item.name == "水果茶").ToList();

            //    if (drinks.Count >= 3)
            //    {
            //        int count = drinks.Sum(drink => int.Parse(drink.amount));

            //        int bonus = count / 3;

            //        Item discount = new Item("(贈送)飲料任買三杯送一杯(送最便宜的)", "0", bonus.ToString(), "0");
            //        items.Add(discount);
            //    }
            //}

            //if (orderType == "全場消費滿350打八折")
            //{
            //    int totalPrice = items.Sum(item => int.Parse(item.total));

            //    if (totalPrice >= 350)
            //    {
            //        double substract = (totalPrice * 0.2);
            //        int sub = (int)substract;

            //        Item discount = new Item("(折扣)全場消費滿350打八折", sub.ToString(), "1", "-" + sub.ToString());
            //        items.Add(discount);
            //    }
            //}

            //if (orderType == "全場消費一律95折")
            //{
            //    int totalPrice = items.Sum(item => int.Parse(item.total));

            //    double substract = (totalPrice * 0.05);
            //    int sub = (int)substract;

            //    Item discount = new Item("(折扣)全場消費一律95折", sub.ToString(), "1", "-" + sub.ToString());
            //    items.Add(discount);
            //}
            ShowPanel.GeneratePanel(items);

        }
    }
}
