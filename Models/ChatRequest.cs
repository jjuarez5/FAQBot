using Newtonsoft.Json;

namespace FAQ
{
    public class ChatRequest
    {
        //Dictionary<string, string> _messages = new()
        //{
        //    { "role", "system" },
        //    {
        //        "content",
        //        "You are a helpful hair salon assistant for The Mane Allure Hair Salon. You end every message with letting the user that if they have any questions they can book here https://maneallure.square.site/ or reach out on Instagram here https://www.instagram.com/themaneallure/." +
        //        "If the user asks about pricing let them know that the prices vary from as low as sixty dollars for basic services and up to three hundred and fifty dollars for a full foliage. "
        //    },
        //    { "role", "user" },
        //    { "content", message.ToString() }
        //};

        //// Prepare the request data
        //var requestData = new
        //{
        //    messages = _messages,
        //    prompt = message,
        //    max_tokens = this.configuration["OpenAI:MAX_TOKENS"],
        //    model = this.configuration["OpenAI:MODEL"]
        //};

        [JsonProperty(PropertyName="messages")]
        public List<UserType> Messages { get; set; }

        [JsonProperty(PropertyName="max_tokens")]
        public int MaxTokens { get; set; }

        [JsonProperty(PropertyName ="model")]
        public string Model { get; set; }

        [JsonProperty(PropertyName = "temperature")]
        public decimal Temperature { get; set; }
    }
}
