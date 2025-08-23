using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OrderSystem.AI.Common.AIRequestModel;
using static OrderSystem.AI.Common.AIResponse;

namespace OrderSystem.AI
{
    public abstract class ATool<T>
    {
        protected T response = default;
        protected Args Args { get; set; }

        public ATool(object respnose)
        {
            string responseString = JsonConvert.SerializeObject(respnose);
            this.response = JsonConvert.DeserializeObject<T>(responseString);
        }

        public abstract object Apply();
    }
}
