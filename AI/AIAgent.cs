using Meziantou.Framework.Win32;
using Newtonsoft.Json;
using OrderSystem.AI.Common;
using OrderSystem.AI.Constant;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static OrderSystem.AI.Common.AIRequestModel;
using static OrderSystem.AI.Common.AIResponse;

namespace OrderSystem
{
    public class AIAgent
    {
        protected static AIRequestModel requestModel = new AIRequestModel();

        //private static readonly HttpClient _httpClient = new HttpClient();

        //private static readonly string _apiKey = ConfigurationManager.AppSettings["apiKey"];
        private static readonly string _apiKey = CredentialManager.ReadCredential(applicationName: "GeminiAppKey").Password;

        public async Task<AIResult> ExecRequest()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent");
            request.Headers.Add("x-goog-api-key", _apiKey);

            string reqString = JsonConvert.SerializeObject(requestModel);
            var content = new StringContent(reqString);
            request.Content = content;

            var response = await (new HttpClient()).SendAsync(request);
            string responseText = await response.Content.ReadAsStringAsync();
            AIResponse aIResponse = JsonConvert.DeserializeObject<AIResponse>(responseText);

            if (aIResponse.candidates[0].content.parts[0].text == null)
            {
                return new AIResult(true, null, aIResponse.candidates[0].content.parts[0].functionCall);
            }
            return new AIResult(false, aIResponse.candidates[0].content.parts[0].text, null);
        }

        public void AddPrompt(Role role, string input)
        {
            AIRequestModel.Content part = new AIRequestModel.Content()
            {
                role = role.ToString(),
                parts = new List<AIRequestModel.Part>()
                {
                    new AIRequestModel.Part(){ text = input},
                }
            };
            requestModel.contents.Add(part);
        }
    }
}
