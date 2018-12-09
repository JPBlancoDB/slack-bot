using Newtonsoft.Json;

namespace SlackBot.Domain.Responses.Channels
{
    public class Info
    {
        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("creator")]
        public string Creator { get; set; }

        [JsonProperty("last_set")]
        public int LastSet { get; set; }
    }
}