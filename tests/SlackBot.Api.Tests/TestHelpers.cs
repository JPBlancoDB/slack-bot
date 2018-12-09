using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Castle.Core.Internal;
using Microsoft.Extensions.Options;
using Moq;
using SlackBot.Domain;
using SlackBot.Domain.Contracts.Factories;
using SlackBot.Domain.Contracts.Services;

namespace SlackBot.Api.Tests
{
    public class TestHelpers
    {
        private static Dictionary<bool, Action<Mock<IQueryStringFactory>, IDictionary<string, string>, string>> queryStringMock =
            new Dictionary<bool, Action<Mock<IQueryStringFactory>, IDictionary<string, string>, string>>
            {
                {false, (queryStringFactory, queryString, userId) => MockCreateQueryStringWithUser(queryStringFactory, queryString, userId)},
                {true, (queryStringFactory, queryString, userId) => MockCreateQueryString(queryStringFactory, queryString, userId)}
            };

        public static Mock<IOptions<SlackConfiguration>> SlackConfiguration()
        {
            var configuration = new Mock<IOptions<SlackConfiguration>>();
            configuration.Setup(s => s.Value).Returns(new SlackConfiguration());
            return configuration;
        }

        public static IDictionary<string, string> QueryStringMockSetup(Mock<IQueryStringFactory> queryStringFactory, string userId = null)
        {
            var queryString = new Dictionary<string, string>();

            queryStringMock[userId.IsNullOrEmpty()].Invoke(queryStringFactory, queryString, userId);

            return queryString;
        }

        private static void MockCreateQueryStringWithUser(Mock<IQueryStringFactory> queryStringFactory, IDictionary<string, string> queryString, string userId)
        {
            queryStringFactory.Setup(
                    s => s.CreateQueryString(It.IsAny<SlackConfiguration>(), userId))
                .Returns(queryString);
        }

        private static void MockCreateQueryString(Mock<IQueryStringFactory> queryStringFactory, IDictionary<string, string> queryString, string userId)
        {
            queryStringFactory.Setup(
                    s => s.CreateQueryString(It.IsAny<SlackConfiguration>()))
                .Returns(queryString);
        }

        public static void RestClientMockSetup<T>(Mock<IRestClient> restClient, IDictionary<string, string> queryString, string action, T responseClass)
        {
            restClient.Setup(s => s.GetAsync<T>(It.IsAny<string>(), action, queryString))
                .ReturnsAsync(responseClass);
        }
    }
}