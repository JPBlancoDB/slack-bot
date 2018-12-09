using Newtonsoft.Json;

namespace SlackBot.Domain.Responses.Channels
{
    public class ResponseMetadata
    {
        [JsonProperty("next_cursor")]
        public string NextCursor { get; set; }
    }
}