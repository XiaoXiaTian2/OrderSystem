using Newtonsoft.Json;
using OrderSystem.Models;
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
            ReceivePanelHandler.RenderPanel += GetOrder;
            flowLayoutPanel1.GetMenu(MenuData.Menus, NumberChanged, CheckChanged);
            comboBox1.DataSource = MenuData.Discounts;
            comboBox1.DisplayMember = "Name";
        }

        private async void NumberChanged(object sender, EventArgs e)
        {
            Console.WriteLine("number changed");
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
                    OrderRequest orderRequest = AIRecommand.Checked ? new OrderRequest(item) : new OrderRequest(item, (MenuModel.Discount)comboBox1.SelectedValue);
                    await Order.AddOrder(orderRequest);
                }
            }
        }

        private void CheckChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox box)
            {
                (box.Tag as NumericUpDown).Value = box.Checked ? 1 : 0;

                //Item item = new Item(
                //    box.Text.Split('$')[0],
                //    box.Text.Split('$')[1],
                //    (box.Tag as NumericUpDown).Value.ToString(),
                //    (int.Parse(box.Text.Split('$')[1]) * (int)((box.Tag as NumericUpDown).Value)).ToString()
                //);
                //OrderRequest orderRequest = AIRecommand.Checked ? new OrderRequest(item) : new OrderRequest(item, (MenuModel.Discount)comboBox1.SelectedValue);
                //await Order.AddOrder(orderRequest);
            }
        }


        private async void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!AIRecommand.Checked)
            {
                OrderRequest orderRequest = new OrderRequest((MenuModel.Discount)comboBox1.SelectedValue);

                await Order.ChangeDiscountType(orderRequest);

            }
        }

        private void GetOrder(object sender, FullSidePanel e)
        {
            flowLayoutPanel2.Controls.Clear();
            flowLayoutPanel2.Controls.Add(e.order);
            textBox1.Text = e.reason;
            Console.WriteLine("get order" + e.reason);
            for (int i = 0; i < MenuData.Discounts.Length; i++)
            {
                if (MenuData.Discounts[i].Name == e.name)
                {
                    comboBox1.SelectedIndex = i;
                    break;
                }
            }

            price.Text = Order.GetPriceTotal();
        }
    }
}
