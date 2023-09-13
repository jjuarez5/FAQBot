
using System.Text;
using Newtonsoft;

namespace FAQ
{
    public class FAQClient : IFAQClient
    {
        private HttpClient client;

        public FAQClient(HttpClient _client)
        {
            this.client = _client;
        }

        // copy url from postman to appSettings
        // use body in postman for referance
        // investigate content for system best practices
        // learn how to feed direct details in the content
        // put model in app settings

        public async Task<Object> GetChatResponseAsync( string userMessage )
        {
            // Prepare the request data
            var requestData = new
            {
                prompt = userMessage,
                max_tokens = 50,
                model = "text-davinci-003"
            };

            string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(requestData);

            var content = new StringContent(
                jsonData,
                Encoding.UTF8,
                "application/json"
            );

            var response = await this.client.PostAsync(this.client.BaseAddress, content);
            var responseContent = await response.Content.ReadAsStringAsync();

            // Extract and return the generated message from the response
            // Adjust this code based on the actual JSON structure of the API response
            // For GPT-3.5, you may need to access responseContent["choices"][0]["message"]["content"]
            return responseContent;
        }
    }
}
