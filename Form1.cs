using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        private string[] mains = { "雞腿飯$85", "排骨飯$120", "雞排飯$95", "香魚飯$100" };
        private string[] adds = { "海帶湯$65", "餛飩湯$100", "味噌湯$75", "蛋花湯$80" };
        private string[] drinks = { "紅茶$45", "綠茶$45", "奶茶$65", "水果茶$50" };
        private string[] deserts = { "冰淇淋$55", "舒芙蕾$60", "冰沙$65", "鬆餅$70" };

        private void Form1_Load(object sender, EventArgs e)
        {
            string[][] menu = { mains, adds, drinks, deserts };
            string[] titles = { "主餐", "附餐", "飲料", "甜點" };
            ReceivePanelHandler.RenderPanel += GetOrder;
            flowLayoutPanel1.GetFlowLayoutPanel(titles, menu, NumberChanged, CheckChanged);
            comboBox1.SelectedIndex = 0;

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
                    Order.AddOrder(comboBox1.Text, item);
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
                Order.AddOrder(comboBox1.Text, item);
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
            Order.ChangeDiscountType(comboBox1.Text);
        }
    }
}
