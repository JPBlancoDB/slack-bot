using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SlackBot.Domain;
using SlackBot.Domain.Contracts.Factories;
using SlackBot.Domain.Contracts.Services;
using SlackBot.Domain.Responses.Channels;

namespace SlackBot.Api.Controllers
{
    [Route("api/[controller]")]
    public class ChannelsController : Controller
    {
        private readonly SlackConfiguration _slackConfiguration;
        private readonly IRestClient _restClient;
        private readonly IQueryStringFactory _queryStringFactory;

        public ChannelsController(IOptions<SlackConfiguration> slackConfiguration, IRestClient restClient, IQueryStringFactory queryStringFactory)
        {
            _slackConfiguration = slackConfiguration.Value;
            _restClient = restClient;
            _queryStringFactory = queryStringFactory;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Channels()
        {
            var queryString = _queryStringFactory.CreateQueryString(_slackConfiguration);

            var result = await _restClient.GetAsync<ChannelListResponse>(_slackConfiguration.ApiUrl, SlackUriConstants.ChannelsList, queryString);

            return Ok(result.Channels);
        }

        [HttpGet("{channelName}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Channel(string channelName)
        {
            var queryString = _queryStringFactory.CreateQueryString(_slackConfiguration);

            var channelList = await _restClient.GetAsync<ChannelListResponse>(_slackConfiguration.ApiUrl, SlackUriConstants.ChannelsList, queryString);

            var channel = channelList.Channels.FirstOrDefault(c => c.Name == channelName);

            if (channel == null)
            {
                return NotFound(channelName);
            }

            return Ok(channel);
        }
    }
}