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
                // create bank of responses to choose from :)
                throw new Exception("Oops!  You can't send an empty message! :)");
            }
            var openAiService = new OpenAIService(new OpenAiOptions()
            {
                ApiKey = this.configuration["OpenAI:API_KEY"]
            });

            openAiService.SetDefaultModelId(this.configuration["OpenAI:MODEL"]);

            string systemMessage = @"You are a helpful hair salon assistant for The Mane Allure Hair Salon. You end every message with making the user aware of either one of the following options, that if they have any questions they can use the booking site and include it for them like so <a href='https://maneallure.square.site/' target='_blank'>Click here</a> or reach out directly to Alma on <a href='https://www.instagram.com/themaneallure/' target=_blank'> Instagram </a>. If the user asks about pricing let them know that the prices vary from as low as sixty dollars for basic services and up to three hundred and fifty dollars for a full foliage.";

            var completionResult = await openAiService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
            {
                Messages = new List<ChatMessage>
                    {
                        ChatMessage.FromSystem(systemMessage),
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
