using OrderSystem.AI.Tools.DiscountSelector.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OrderSystem.AI.Common.AIRequestModel;

namespace OrderSystem.AI.Tools.DiscountSelector.Request
{
    internal class DiscountSelectorDeclaration : Functiondeclaration
    {
        public DiscountSelectorDeclaration() { }

        public override string name => "OrderSystem.AI.Tools.DiscountSelector.DiscountSelectorTool";

        public override string description => "選擇折扣策略";

        public override Parameters parameters
        {
            get => new DiscountSelectorParameters();
        }
    }
}
