using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderSystem
{
    class ReceivePanelHandler
    {
        public static event EventHandler<FlowLayoutPanel> RenderPanel;

        public static void GetFlowPanel(FlowLayoutPanel flowLayoutPanel)
        {
            RenderPanel.Invoke(null, flowLayoutPanel);
        }
    }
}
