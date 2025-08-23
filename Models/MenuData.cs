using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OrderSystem.MenuModel;

namespace OrderSystem.Models
{
    internal class MenuData
    {
        public static Menu[] Menus { get; set; }
        public static MenuModel.Discount[] Discounts { get; set; }
        public static string MenuJSON { get; set; }

        static MenuData()
        {
            string menuPath = ConfigurationManager.AppSettings["Menu"].ToString();
            MenuJSON = File.ReadAllText(menuPath);
            MenuModel model = JsonConvert.DeserializeObject<MenuModel>(MenuJSON);
            Menus = model.Menus;
            Discounts = model.Discounts;
        }
    }
}
