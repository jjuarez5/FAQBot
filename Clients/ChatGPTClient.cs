using FAQ.Models;
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
            if (string.IsNullOrEmpty(query.UserMessage))
            {
                // consider handling on the front end for perf
                // pick a random empty string error message
                Random random = new Random();
                int randomIndex = random.Next(0, ErrorMessages.NoInputErrors.Count);
                throw new Exception(ErrorMessages.NoInputErrors[randomIndex]);
            }
            var openAiService = new OpenAIService(new OpenAiOptions()
            {
                ApiKey = this.configuration["OpenAI:API_KEY"]
            });

            openAiService.SetDefaultModelId(this.configuration["OpenAI:MODEL"]);

            // make a constants page and add there as well as configurable prompts

            var completionResult = await openAiService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
            {
                Messages = new List<ChatMessage>
                    {
                        ChatMessage.FromSystem(Constants.ManeAllurePrompt),
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
