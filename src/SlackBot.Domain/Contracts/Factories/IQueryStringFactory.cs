using System.Collections.Generic;

namespace SlackBot.Domain.Contracts.Factories
{
    public interface IQueryStringFactory
    {
        IDictionary<string, string> CreateQueryString(SlackConfiguration slackConfiguration);

        IDictionary<string, string> CreateQueryString(SlackConfiguration slackConfiguration, string userId);
    }
}