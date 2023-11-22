using OpenAI;
using OpenAI.Managers;
using OpenAI.ObjectModels.RequestModels;

namespace FAQ
{
    public class ChatGPTClient
    {
        private readonly IConfiguration configuration;
        public ChatGPTClient(IConfiguration _configuration )
        {
            this.configuration = _configuration;
        }

        // use configuration to input system, api key, model and tokens , etc... -- DONE
        // use the classes to make it into json data?
        public async Task<string> GetChatCompletionAsync( Message query )
        {
            var openAiService = new OpenAIService(new OpenAiOptions()
            {
                ApiKey = this.configuration["OpenAI:API_KEY"]
            });

            openAiService.SetDefaultModelId(this.configuration["OpenAI:MODEL"]);

            var completionResult = await openAiService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
            {
                Messages = new List<ChatMessage>
                    {
                        ChatMessage.FromSystem(this.configuration["OpenAI:SYSTEM_MESSAGE"]),
                        ChatMessage.FromUser(query.UserMessage),
                    },
                MaxTokens = int.Parse(this.configuration["OpenAI:MAX_TOKENS"]),
                Temperature = float.Parse(this.configuration["OpenAI:TEMPERATURE"])
            }).ConfigureAwait(false);
            try
            {
                if (completionResult.Successful)
                {
                    return completionResult.Choices.First().Message.Content;
                }
                else
                {
                    if (completionResult.Error == null)
                    {
                        throw new Exception("Unknown Error");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            
            return null;
        }
    }
}
