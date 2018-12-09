using System.Collections.Generic;
using SlackBot.Domain;
using SlackBot.Domain.Contracts.Factories;

namespace SlackBot.Api.Factories
{
    public class QueryStringFactory : IQueryStringFactory
    {
        public IDictionary<string, string> CreateQueryString(SlackConfiguration slackConfiguration, string userId)
        {
            var queryString = CreateQueryString(slackConfiguration);

            queryString.Add("user", userId);

            return queryString;
        }
        
        public IDictionary<string, string> CreateQueryString(SlackConfiguration slackConfiguration)
        {
            var queryString = new Dictionary<string, string>
            {
                { "token", slackConfiguration.BotToken }
            };
            
            return queryString;
        }
    }
}