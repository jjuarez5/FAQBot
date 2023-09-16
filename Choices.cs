using Newtonsoft.Json;

namespace FAQ
{
    public class Choices
    {
        [JsonProperty(PropertyName="choices")]
        public List<object> Choice { get; set; }
    }
}