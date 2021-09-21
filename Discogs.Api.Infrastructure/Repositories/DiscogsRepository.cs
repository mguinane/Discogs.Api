using System;
using System.Threading.Tasks;
using Discogs.Api.Core.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Discogs.Api.Core.Repositories;
using Discogs.Api.Infrastructure.Repositories.Helpers;
using System.Net.Mime;

namespace Discogs.Api.Infrastructure.Repositories
{
    public class DiscogsRepository : IDiscogsRepository
    {
        private readonly HttpClient _httpClient;

        public DiscogsRepository(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<Collection> GetCollectionAsync(SearchCriteria criteria)
        {
            Collection collection = null;

            var response = await _httpClient.GetAsync(UriHelper.FormatCollectionRequestUri(criteria));

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                collection = JsonConvert.DeserializeObject<Collection>(result);
            }

            return collection;
        }

        public async Task<Wantlist> GetWantlistAsync(SearchCriteria criteria)
        {
            Wantlist wantlist = null;

            var response = await _httpClient.GetAsync(UriHelper.FormatWantlistRequestUri(criteria));

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                wantlist = JsonConvert.DeserializeObject<Wantlist>(result);
            }

            return wantlist;
        }
    }
}
