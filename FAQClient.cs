
using System.Text;
using Newtonsoft;
using Newtonsoft.Json.Linq;
using OpenAI.ObjectModels.RequestModels;

namespace FAQ
{
    public class FAQClient : IFAQClient
    {
        private HttpClient client;
        private IConfiguration configuration;

        public FAQClient( HttpClient _client, IConfiguration _configuration )
        {
            this.client = _client;
            this.configuration = _configuration;
        }

        // create message model
        // create messages model

        /* https://api.openai.com/v1/chat/completions
         *  {  
        "model":"gpt-3.5-turbo",
        "max_tokens":50,
        "messages":[
         {
             "role":"system",
             "content": "You are a helpful hair salon virtual assistant."
         },
         {
             "role":"user",
             "content": "Hi, I'm looking for a haircut!"
         }
        ]
        }
        */

        public async Task<Object> GetChatResponseAsync( Message message )
        {
            UserType system = new()
            {
                Role = "system",
                Content = "You are a helpful hair salon assistant for The Mane Allure Hair Salon. You end every message with making the user aware of either one of the following options, that if they have any questions they can use the booking site or reach out directly to Alma on Instagram at https://www.instagram.com/themaneallure/. If the user asks about pricing let them know that the prices vary from as low as sixty dollars for basic services and up to three hundred and fifty dollars for a full foliage."
            };

            UserType user = new()
            {
                Role = "user",
                Content = message.UserMessage
            };

            List<UserType> _users = new List<UserType>()
            {
                system,
                user
            };

            ChatRequest chatRequest = new ChatRequest()
            {
                Messages = _users,
                MaxTokens = int.Parse(this.configuration["OpenAI:MAX_TOKENS"]),
                Model = this.configuration["OpenAI:MODEL"],
                Temperature = decimal.Parse(this.configuration["OpenAI:TEMPERATURE"])
            };

            string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(chatRequest);

            var content = new StringContent(
                jsonData,
                Encoding.UTF8,
                "application/json"
            );

            var response = await this.client.PostAsync(this.client.BaseAddress, content);
            var responseContent = await response.Content.ReadAsStringAsync();
            JObject jObject = JObject.Parse(responseContent);
            List<object> items = new List<object>();
            ChatResponse respy = new ChatResponse();
            Usage usage = new Usage();

            respy.Id = jObject.Property("id").Value.ToString();
            respy.Object = jObject.Property("object").Value.ToString();
            respy.Created = ((int)jObject.Property("created").Value);
            respy.Model = jObject.Property("model").Value.ToString();
            respy.Usage = Newtonsoft.Json.JsonConvert.DeserializeObject<Usage>(jObject.Property("usage").Value.ToString());
            dynamic dynamic = jObject.Property("choices").Children()[0]["message"]["content"];
            // List<object> choice = Newtonsoft.Json.JsonConvert.DeserializeObject(jObject.Property("choices").Value.ToString());
            List<object> newList = new()
            {
                dynamic
            };

            return newList[0];






            //var chatResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<ChatResponse>(responseContent);            

            // Extract and return the generated message from the response
            // Adjust this code based on the actual JSON structure of the API response
            // For GPT-3.5, you may need to access responseContent["choices"][0]["message"]["content"]
        }
    }
}
