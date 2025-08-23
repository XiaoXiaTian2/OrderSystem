using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Models
{
    public class OrderRequest
    {
        public bool AIRecommand => DiscountType == null;
        public MenuModel.Discount DiscountType { get; set; } = null;
        public Item MenuItem { get; set; }
        public List<Item> OrderList { get; set; }

        public OrderRequest(Item MenuItem)
        {
            this.MenuItem = MenuItem;
        }

        public OrderRequest(MenuModel.Discount DiscountType)
        {
            this.DiscountType = DiscountType;
        }

        public OrderRequest(Item MenuItem, MenuModel.Discount DiscountType)
        {
            this.MenuItem = MenuItem;
            this.DiscountType = DiscountType;
        }
    }
}
