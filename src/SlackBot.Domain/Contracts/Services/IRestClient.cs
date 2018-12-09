using System.Collections.Generic;
using System.Threading.Tasks;

namespace SlackBot.Domain.Contracts.Services
{
    public interface IRestClient
    {
        Task<T> GetAsync<T>(string baseUrl, string action, IDictionary<string, string> queryString);
    }
}