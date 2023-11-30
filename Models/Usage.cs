using Newtonsoft.Json;

namespace FAQ
{
    public class Usage
    {
        [JsonProperty(PropertyName="prompt_tokens")]
        public int PromptTokens { get; set; }

        [JsonProperty(PropertyName="completion_tokens")]
        public int CompletionTokens { get; set; }

        [JsonProperty(PropertyName ="total_tokens")]
        public int TotalTokens { get; set; }

    }
}
