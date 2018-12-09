using Newtonsoft.Json;

namespace SlackBot.Domain.Responses.Channels
{
    public class Channel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("is_channel")]
        public bool IsChannel { get; set; }

        [JsonProperty("created")]
        public int Created { get; set; }
        
        [JsonProperty("is_archived")]
        public bool IsArchived { get; set; }

        [JsonProperty("is_general")]
        public bool IsGeneral { get; set; }

        [JsonProperty("unlinked")]
        public int UnLinked { get; set; }

        [JsonProperty("creator")]
        public string Creator { get; set; }

        [JsonProperty("name_normalized")]
        public string NameNormalized { get; set; }
        
        [JsonProperty("is_shared")]
        public bool IsShared { get; set; }
        
        [JsonProperty("is_org_shared")]
        public bool IsOrgShared { get; set; }
        
        [JsonProperty("is_member")]
        public bool IsMember { get; set; }
        
        [JsonProperty("is_private")]
        public bool IsPrivate { get; set; }
        
        [JsonProperty("IsMpim")]
        public bool IsMpim { get; set; }

        [JsonProperty("members")]
        public string[] Members { get; set; }

        [JsonProperty("topic")]
        public Info Topic { get; set; }

        [JsonProperty("purpose")]
        public Info Purpose { get; set; }

        [JsonProperty("previous_names")]
        public string[] PreviousNames { get; set; }

        [JsonProperty("num_members")]
        public int NumMembers { get; set; }
    }    
}