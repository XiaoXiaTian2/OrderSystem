using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderSystem
{
    public static class Extension
    {
        public static int GetDigitalCount(this string input)
        {
            int count = 0;
            foreach (char chr in input)
            {
                if (Char.IsDigit(chr))
                {
                    count++;
                }
            }

            return count;
        }

        public static FlowLayoutPanel GetFlowLayoutPanel(this FlowLayoutPanel panel, string[] titles, string[][] menu, EventHandler NumberChanged, EventHandler CheckChanged)
        {
            for (int i = 0; i < titles.Length; i++)
            {
                FlowLayoutPanel eachPanel = new FlowLayoutPanel
                {
                    Size = new Size(225, 257),
                    BorderStyle = BorderStyle.Fixed3D
                };

                int yi = 10;

                Label label = new Label
                {
                    Text = titles[i],
                    Padding = new Padding(5, 10, 0, 0),
                    Size = new Size(200, 30)
                };
                eachPanel.Controls.Add(label);
                for (int j = 0; j < 4; j++)
                {
                    CheckBox checkBox = new CheckBox
                    {
                        Text = menu[i][j],
                        Padding = new Padding(5, 0, 0, 0),
                        Size = new Size(100, 20),
                        Location = new Point(0, yi),
                    };
                    yi += 30;
                    NumericUpDown numericUpDown = new NumericUpDown
                    {
                        Size = new Size(40, 0),
                        Tag = checkBox
                    };
                    checkBox.Tag = numericUpDown;
                    checkBox.CheckedChanged += CheckChanged;

                    numericUpDown.ValueChanged += NumberChanged;
                    eachPanel.Controls.Add(checkBox);
                    eachPanel.Controls.Add(numericUpDown);
                }
                panel.Controls.Add(eachPanel);
            }
            return panel;
        }

        public static FlowLayoutPanel GetMenu(this FlowLayoutPanel panel, MenuModel.Menu[] menus, EventHandler NumberChanged, EventHandler CheckChanged)
        {
            foreach (var item in menus)
            {
                var title = item.Title;
                var items = item.Items;

                FlowLayoutPanel eachPanel = new FlowLayoutPanel
                {
                    Size = new Size(225, 257),
                    BorderStyle = BorderStyle.Fixed3D
                };
                int yi = 10;

                Label label = new Label
                {
                    Text = title,
                    Padding = new Padding(5, 10, 0, 0),
                    Size = new Size(200, 30)
                };
                eachPanel.Controls.Add(label);

                foreach (var item1 in items)
                {
                    CheckBox checkBox = new CheckBox
                    {
                        Text = item1.Name + '$' + item1.Price,
                        Padding = new Padding(5, 0, 0, 0),
                        Size = new Size(100, 20),
                        Location = new Point(0, yi),
                    };
                    yi += 30;
                    NumericUpDown numericUpDown = new NumericUpDown
                    {
                        Size = new Size(40, 0),
                        Tag = checkBox
                    };
                    checkBox.Tag = numericUpDown;
                    checkBox.CheckedChanged += CheckChanged;

                    numericUpDown.ValueChanged += NumberChanged;
                    eachPanel.Controls.Add(checkBox);
                    eachPanel.Controls.Add(numericUpDown);
                }
                panel.Controls.Add(eachPanel);
            }
            return panel;
        }


    }
}
