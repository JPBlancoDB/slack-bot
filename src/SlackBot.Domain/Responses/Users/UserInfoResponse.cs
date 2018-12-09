using Newtonsoft.Json;

namespace SlackBot.Domain.Responses.Users
{
    public class UserInfoResponse
    {
        [JsonProperty("ok")]
        public bool Ok { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }
    }
}