using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem
{
    public class MenuModel
    {
        public Menu[] Menus { get; set; }
        public Discount[] Discounts { get; set; }


        public class Menu
        {
            public string Title { get; set; }
            public Item[] Items { get; set; }
        }

        public class Item
        {
            public string Name { get; set; }
            public int Price { get; set; }
        }

        public class Discount
        {
            public string Name { get; set; }
            public string Strategy { get; set; }
            public Condition[] Conditions { get; set; }
            public Reward[] Rewards { get; set; }
        }

        public class Condition
        {
            public string Name { get; set; }
            public int Number { get; set; }
            public int Total_Price { get; set; }
        }

        public class Reward
        {
            public string Name { get; set; }
            public int Number { get; set; }
            public string Free_type { get; set; }
            public int Discount_Price { get; set; }
            public float Discount_Percentage { get; set; }
            public int Set_Price { get; set; }
        }

    }
}
