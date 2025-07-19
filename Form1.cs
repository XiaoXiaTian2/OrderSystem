using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OrderSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            string menuPath = ConfigurationManager.AppSettings["Menu"].ToString();
            string info = File.ReadAllText(menuPath);
            MenuModel model = JsonConvert.DeserializeObject<MenuModel>(info);
            ReceivePanelHandler.RenderPanel += GetOrder;
            flowLayoutPanel1.GetMenu(model, NumberChanged, CheckChanged);
            comboBox1.DataSource = model.Discounts;
            comboBox1.DisplayMember = "Name";
        }

        private void NumberChanged(object sender, EventArgs e)
        {
            if (sender is NumericUpDown numericUpDown)
            {
                CheckBox box = numericUpDown.Tag as CheckBox;
                box.Checked = numericUpDown.Value > 0;
                if (box.Checked)
                {
                    Item item = new Item(
                        box.Text.Split('$')[0],
                        box.Text.Split('$')[1],
                        numericUpDown.Value.ToString(),
                        (int.Parse(box.Text.Split('$')[1]) * (int)(numericUpDown.Value)).ToString()
                    );
                    Order.AddOrder((MenuModel.Discount)comboBox1.SelectedValue, item);
                }
            }
        }

        private void CheckChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox box)
            {
                (box.Tag as NumericUpDown).Value = box.Checked ? 1 : 0;
                Item item = new Item(
                    box.Text.Split('$')[0],
                    box.Text.Split('$')[1],
                    (box.Tag as NumericUpDown).Value.ToString(),
                    (int.Parse(box.Text.Split('$')[1]) * (int)((box.Tag as NumericUpDown).Value)).ToString()
                );
                Order.AddOrder((MenuModel.Discount)comboBox1.SelectedValue, item);
            }
        }

        private void GetOrder(object sender, FlowLayoutPanel e)
        {
            flowLayoutPanel2.Controls.Clear();
            flowLayoutPanel2.Controls.Add(e);
            price.Text = Order.GetPriceTotal();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Order.ChangeDiscountType((MenuModel.Discount)comboBox1.SelectedValue);
        }
    }
}
