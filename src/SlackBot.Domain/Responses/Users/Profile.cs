using Newtonsoft.Json;

namespace SlackBot.Domain.Responses.Users
{
    public class Profile
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("skype")]
        public string Skype { get; set; }

        [JsonProperty("real_name")]
        public string RealName { get; set; }

        [JsonProperty("real_name_normalized")]
        public string RealNameNormalized { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }
        
        [JsonProperty("display_name_normalized")]
        public string DisplayNameNormalized { get; set; }

        [JsonProperty("status_text")]
        public string StatusText { get; set; }

        [JsonProperty("status_emoji")]
        public string StatusEmoji { get; set; }

        [JsonProperty("status_expiration")]
        public int StatusExpiration { get; set; }

        [JsonProperty("avatar_hash")]
        public string AvatarHash { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("image_24")]
        public string Image24 { get; set; }
        
        [JsonProperty("image_32")]
        public string Image32 { get; set; }
        
        [JsonProperty("image_48")]
        public string Image48 { get; set; }
        
        [JsonProperty("image_72")]
        public string Image72 { get; set; }
        
        [JsonProperty("image_192")]
        public string Image192 { get; set; }
        
        [JsonProperty("image_512")]
        public string Image512 { get; set; }

        [JsonProperty("status_text_canonical")]
        public string StatusTextCanonical { get; set; }

        [JsonProperty("team")]
        public string Team { get; set; }
    }
}