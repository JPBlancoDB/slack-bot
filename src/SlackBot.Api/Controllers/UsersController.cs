using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SlackBot.Domain;
using SlackBot.Domain.Contracts.Factories;
using SlackBot.Domain.Contracts.Services;
using SlackBot.Domain.Responses.Users;

namespace SlackBot.Api.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly SlackConfiguration _slackConfiguration;
        private readonly IRestClient _restClient;
        private readonly IQueryStringFactory _queryStringFactory;
        
        public UsersController(IOptions<SlackConfiguration> slackConfiguration, IRestClient restClient, IQueryStringFactory queryStringFactory)
        {
            _slackConfiguration = slackConfiguration.Value;
            _restClient = restClient;
            _queryStringFactory = queryStringFactory;
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UserInfo(string userId)
        {
            var queryString = _queryStringFactory.CreateQueryString(_slackConfiguration, userId);

            var userInfo = await _restClient.GetAsync<UserInfoResponse>(_slackConfiguration.ApiUrl, SlackUriConstants.UserInfo, queryString);

            if (userInfo.Ok)
            {
                return Ok(userInfo.User);
            }

            return NotFound(userId);
        }
    }
}