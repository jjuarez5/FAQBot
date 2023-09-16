using Newtonsoft.Json;

namespace FAQ
{
    public class ChatResponse
    {
        [JsonProperty(PropertyName="id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName ="object")]
        public string Object { get; set; }

        [JsonProperty(PropertyName = "created")]
        public int Created { get; set; }

        [JsonProperty(PropertyName = "model")]
        public string Model { get; set; }

        [JsonProperty(PropertyName = "choices")]
        public List<object> Choices { get; set; }

        [JsonProperty(PropertyName = "usage")]
        public Usage Usage { get; set; }



    }
}
