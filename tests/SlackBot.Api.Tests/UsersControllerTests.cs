using System.Collections.Generic;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SlackBot.Api.Controllers;
using SlackBot.Domain;
using SlackBot.Domain.Contracts.Factories;
using SlackBot.Domain.Contracts.Services;
using SlackBot.Domain.Responses.Users;
using Xunit;

namespace SlackBot.Api.Tests
{
    public class UsersControllerTests
    {
        private readonly Mock<IRestClient> _restClient;
        private readonly Mock<IQueryStringFactory> _queryStringFactory;
        private readonly UsersController _controller;
        private const string UserId = "userId";

        public UsersControllerTests()
        {
            var configuration = TestHelpers.SlackConfiguration();
            _restClient = new Mock<IRestClient>();
            _queryStringFactory = new Mock<IQueryStringFactory>();
            _controller = new UsersController(configuration.Object, _restClient.Object, _queryStringFactory.Object);
        }

        [Fact]
        public async Task UserInfo_ShouldReturnNotFound_WhenSlackApiDoesNotReturnUser()
        {
            //Arrange
            var userInfoResponse = Builder<UserInfoResponse>
                .CreateNew()
                .With(w => w.Ok = false)
                .Build();

            SetupMocks(userInfoResponse);

            //Act
            var result = await _controller.UserInfo(UserId);

            //Assert
            var notFoundObjectResult = AssertResult<NotFoundObjectResult>(UserId, result);
            Assert.Equal(UserId, notFoundObjectResult.Value);
        }

        [Fact]
        public async Task UserInfo_ShoudlReturnUser()
        {
            //Arrange
            const string userId = "userId";
            var user = Builder<User>.CreateNew().Build();
            var userInfoResponse = Builder<UserInfoResponse>
                .CreateNew()
                .With(w => w.Ok = true)
                .With(w => w.User = user)
                .Build();

            SetupMocks(userInfoResponse);

            //Act
            var result = await _controller.UserInfo(userId);

            //Assert
            var actionResult = AssertResult<OkObjectResult>(userId, result);
            var returnValue = Assert.IsType<User>(actionResult.Value);
            Assert.Equal(user, returnValue);
        }

        private T AssertResult<T>(string userId, IActionResult result)
        {
            _restClient.Verify(v => v.GetAsync<UserInfoResponse>(It.IsAny<string>(), SlackUriConstants.UserInfo, It.IsAny<Dictionary<string, string>>()),
                Times.Once);
            _queryStringFactory.Verify(v => v.CreateQueryString(It.IsAny<SlackConfiguration>(), userId), Times.Once);
            return Assert.IsType<T>(result);
        }

        private void SetupMocks(UserInfoResponse userInfoResponse)
        {
            var queryString = TestHelpers.QueryStringMockSetup(_queryStringFactory, UserId);

            TestHelpers.RestClientMockSetup(_restClient, queryString, SlackUriConstants.UserInfo, userInfoResponse);
        }
    }
}