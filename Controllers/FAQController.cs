namespace FAQ
{
    using Microsoft.AspNetCore.Mvc;


    [Route("api/[controller]")]
    [ApiController]
    public class FAQController : ControllerBase
    {
        private HttpClient _httpClient;
        private IConfiguration _configuration;
        private readonly FAQClient _client;

        public FAQController( HttpClient client, IConfiguration configuration)
        {
            this._httpClient = client;
            this._configuration = configuration;
            this._client = new FAQClient(this._httpClient, this._configuration);
        }

        [HttpPost]
        public async Task<object> GetFAQAnswer( [FromBody] Message message)
        {
            var response = await this._client.GetChatResponseAsync(message).ConfigureAwait(false);

            return response;

        }
    }


}
