using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SlackBot.Domain.Contracts.Services;

namespace SlackBot.Api.Services
{
    public class RestClient : IRestClient
    {
        public async Task<T> GetAsync<T>(string baseUrl, string action, IDictionary<string, string> queryString)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };

            var requestUri = CreateRequestUri(action, queryString);
    
            var result = await client.GetAsync(requestUri);

            return JsonConvert.DeserializeObject<T>(result.Content.ReadAsStringAsync().Result);
        }
        
        private string CreateRequestUri(string action, IDictionary<string, string> queryString)
        {
            var query = queryString.Aggregate("", (current, item) => current + $"{item.Key}={item.Value}&");

            return $"{action}?{query}";
        }
    }
}