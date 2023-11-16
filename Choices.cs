using Newtonsoft.Json;

namespace FAQ
{
    public class Choices
    {
        [JsonProperty(PropertyName="index")]
        public int Index { get; set; }

        [JsonProperty(PropertyName="message")]
        public UserType Message { get; set; }

        [JsonProperty(PropertyName = "finish_reason")]
        public string FinishReason { get; set; }
    }
}