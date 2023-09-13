namespace FAQ
{
    using Microsoft.AspNetCore.Mvc;


    [Route("api/[controller]")]
    [ApiController]
    public class FAQController : ControllerBase
    {
        private HttpClient _httpClient;
        private readonly FAQClient _client;

        public FAQController( HttpClient client )
        {
            this._httpClient = client;
            this._client = new FAQClient(this._httpClient);
        }

        [HttpPost]
        public async Task<object> GetFAQAnswer( [FromBody] string message )
        {
            var response = await this._client.GetChatResponseAsync(message).ConfigureAwait(false);

            return response;

        }
    }


}
