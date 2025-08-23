using OrderSystem.AI.Tools.DiscountSelector.Request;
using OrderSystem.AI.Tools.DiscountSelector.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.AI.Tools.DiscountSelector
{
    internal class DiscountSelectorTool : ATool<DiscountSelectorResponseArgs>
    {
        public DiscountSelectorTool(object args) : base(args)
        {
        }

        public override object Apply()
        {
            Console.WriteLine($"取得折扣，名稱: {response.name}, 策略: {response.strategy}, 原因: {response.reason}"); ;
            return response;
        }
    }
}
