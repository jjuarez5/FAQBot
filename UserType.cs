using Newtonsoft.Json;

namespace FAQ
{
    public class UserType
    {
        [JsonProperty(PropertyName="role")]
        public string Role { get; set; }

        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }
    }
}