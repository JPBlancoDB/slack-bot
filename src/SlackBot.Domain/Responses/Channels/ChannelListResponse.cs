using System.Collections.Generic;
using Newtonsoft.Json;

namespace SlackBot.Domain.Responses.Channels
{
    public class ChannelListResponse
    {
        [JsonProperty("ok")]
        public bool Ok { get; set; }

        [JsonProperty("channels")]
        public IEnumerable<Channel> Channels { get; set; }

        [JsonProperty("response_metadata")]
        public ResponseMetadata ResponseMetadata { get; set; }
    }
}