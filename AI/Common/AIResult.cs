using OrderSystem.AI.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static OrderSystem.AI.Common.AIResponse;

namespace OrderSystem.AI.Common
{
    public class AIResult
    {
        public AIResult(bool canUseTool, string text, Functioncall functioncall)
        {
            this.CanUseTool = canUseTool;
            this.Text = text;
            this.functioncall = functioncall;
        }

        public bool CanUseTool { get; set; }
        public string Text { get; set; }
        private Functioncall functioncall { get; set; }

        public object UseTool()
        {
            string name = functioncall.name;
            Console.WriteLine(JsonConvert.SerializeObject(functioncall));

            Type toolType = Type.GetType(name);
            object strategy = Activator.CreateInstance(toolType, new object[] { functioncall.args });

            MethodInfo applyFunc = strategy.GetType().GetMethod("Apply", BindingFlags.Public | BindingFlags.Instance);
            return applyFunc.Invoke(strategy, null);
        }
    }
}
