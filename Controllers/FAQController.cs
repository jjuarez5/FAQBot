namespace FAQ
{
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class FAQController : ControllerBase
    {
        private IConfiguration _configuration;
        private readonly ChatGPTClient _chatGPTClient;

        public FAQController(IConfiguration configuration,  ChatGPTClient chatGPTClient)
        {
            this._configuration = configuration;
            this._chatGPTClient = new ChatGPTClient(this._configuration);
        }

        [HttpPost]
        public async Task<object> GetFAQAnswer( [FromBody] Message message)
        {
            string response = string.Empty;
            try
            {
                response = await this._chatGPTClient.GetChatCompletionAsync(message);
            }
            catch (Exception ex)
            {

                return ex.Message;
            }

            return response;

        }
    }


}
