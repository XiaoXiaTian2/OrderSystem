using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderSystem
{
    class ShowPanel
    {
        public static void GeneratePanel(List<Item> orderDetails)
        {
            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel
            {
                Size = new Size(350, 600)
            };
            string[] labels = { "品名", "單價", "數量", "小計" };
            foreach (var item in labels)
            {
                Label label = new Label
                {
                    Text = item,
                    Size = new Size(70, 30),
                    Padding = new Padding(5, 10, 0, 0),

                };
                flowLayoutPanel.Controls.Add(label);
            }

            foreach (var item1 in orderDetails)
            {
                Type itemType = item1.GetType();
                PropertyInfo[] properties = itemType.GetProperties();
                foreach (var property in properties)
                {
                    Label label = new Label
                    {
                        Text = property.GetValue(item1).ToString(),
                        Size = new Size(70, 30),
                        Padding = new Padding(5, 10, 0, 0),
                    };
                    flowLayoutPanel.Controls.Add(label);
                }
            }

            ReceivePanelHandler.GetFlowPanel(flowLayoutPanel);
        }
    }
}
