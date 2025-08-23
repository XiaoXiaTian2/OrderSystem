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
        // 08/16 HW 跟理由區打包成物件
        public static event EventHandler<FullSidePanel> RenderPanel;

        public static void GetFlowPanel(FullSidePanel fullSidePanel)
        {
            RenderPanel.Invoke(null, fullSidePanel);
        }
    }
}
