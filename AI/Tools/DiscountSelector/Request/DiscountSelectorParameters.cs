using OrderSystem.AI.Tools.DiscountSelector.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OrderSystem.AI.Common.AIRequestModel;

namespace OrderSystem.AI.Tools.DiscountSelector.Request
{
    internal class DiscountSelectorParameters : Parameters
    {
        public override string type { get => "object"; }

        public override object properties
        {
            get => new DiscountSelectorProperties()
            {
                name = new Args()
                {
                    type = "string",
                    description = "折扣名稱"
                },
                strategy = new Args()
                {
                    type = "string",
                    description = "折扣策略"
                },
                reason = new Args()
                {
                    type = "string",
                    description = "選擇的理由"
                },
            };
        }

        public override List<string> required
        {
            get => new List<string>()
                {
                    "name",
                    "strategy",
                    "reason"
                };
        }

    }
}
