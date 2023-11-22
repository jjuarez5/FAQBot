namespace FAQ
{
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class FAQController : ControllerBase
    {
        //private HttpClient _httpClient;
        private IConfiguration _configuration;
        private readonly FAQClient _client;
        private readonly ChatGPTClient _chatGPTClient;

        public FAQController(IConfiguration configuration,  ChatGPTClient chatGPTClient)
        {
            //this._httpClient = client;
            this._configuration = configuration;
            //this._client = new FAQClient(this._httpClient, this._configuration);
            //this._openAiClient = openAiClient;
            this._chatGPTClient = new ChatGPTClient(this._configuration);
        }

        [HttpPost]
        public async Task<object> GetFAQAnswer( [FromBody] Message message)
        {
            var response = await this._chatGPTClient.GetChatCompletionAsync(message);

            return response;

        }
    }


}
