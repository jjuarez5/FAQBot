using Newtonsoft.Json;

namespace FAQ
{
    public class Message
    {
        [JsonProperty(PropertyName="message")]
        public string UserMessage { get; set; }
    }
}
