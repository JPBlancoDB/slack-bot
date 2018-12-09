using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SlackBot.Api.Controllers;
using SlackBot.Domain;
using SlackBot.Domain.Contracts.Factories;
using SlackBot.Domain.Contracts.Services;
using SlackBot.Domain.Responses.Channels;
using Xunit;

namespace SlackBot.Api.Tests
{
    public class ChannelsControllerTests
    {
        private readonly Mock<IRestClient> _restClient;
        private readonly Mock<IQueryStringFactory> _queryStringFactory;
        private readonly ChannelsController _controller;

        public ChannelsControllerTests()
        {
            var configuration = TestHelpers.SlackConfiguration();
            _restClient = new Mock<IRestClient>();
            _queryStringFactory = new Mock<IQueryStringFactory>();
            _controller = new ChannelsController(configuration.Object, _restClient.Object, _queryStringFactory.Object);
        }

        [Fact]
        public async Task Channels_ShouldReturnListOfChannels()
        {
            //Arrange
            var channels = Builder<Channel>.CreateListOfSize(2).Build();
            var channelListResponse = Builder<ChannelListResponse>
                .CreateNew()
                .With(w => w.Ok = true)
                .With(w => w.Channels = channels)
                .Build();

            SetupMocks(channelListResponse);

            //Act
            var result = await _controller.Channels();

            //Assert
            var actionResult = AssertResult<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<Channel>>(actionResult.Value);
            Assert.Equal(channels, returnValue);
        }

        [Fact]
        public async Task Channel_ShoudlReturnChannel_WhenChannelNameIsProvided()
        {
            //Arrange
            const string channelName = "channelName";
            var channels = Builder<Channel>
                .CreateListOfSize(2)
                .Random(1)
                .With(w => w.Name = channelName)
                .Build();
            var channelListResponse = Builder<ChannelListResponse>
                .CreateNew()
                .With(w => w.Ok = true)
                .With(w => w.Channels = channels)
                .Build();

            SetupMocks(channelListResponse);

            //Act
            var result = await _controller.Channel(channelName);

            //Assert
            var actionResult = AssertResult<OkObjectResult>(result);
            var returnValue = Assert.IsType<Channel>(actionResult.Value);
            Assert.Equal(channels.FirstOrDefault(w => w.Name == channelName), returnValue);
        }

        [Fact]
        public async Task Channel_ShoudlReturnNotFound_WhenChannelNameDoesNotExist()
        {
            //Arrange
            const string channelName = "channelName";
            var channels = Builder<Channel>.CreateListOfSize(2).Build();
            var channelListResponse = Builder<ChannelListResponse>
                .CreateNew()
                .With(w => w.Ok = true)
                .With(w => w.Channels = channels)
                .Build();

            SetupMocks(channelListResponse);

            //Act
            var result = await _controller.Channel(channelName);

            //Assert
            var notFoundObjectResult = AssertResult<NotFoundObjectResult>(result);
            Assert.Equal(channelName, notFoundObjectResult.Value);
        }

        private void SetupMocks(ChannelListResponse channelListResponse)
        {
            var queryString = TestHelpers.QueryStringMockSetup(_queryStringFactory);

            TestHelpers.RestClientMockSetup(_restClient, queryString, SlackUriConstants.ChannelsList, channelListResponse);
        }

        private T AssertResult<T>(IActionResult result)
        {
            _restClient.Verify(v => v.GetAsync<ChannelListResponse>(It.IsAny<string>(), SlackUriConstants.ChannelsList, It.IsAny<Dictionary<string, string>>()),
                Times.Once);
            _queryStringFactory.Verify(v => v.CreateQueryString(It.IsAny<SlackConfiguration>()), Times.Once);
            var actionResult = Assert.IsType<T>(result);
            return actionResult;
        }
    }
}