using System;
using System.Threading.Tasks;
using Discogs.Api.Core.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Discogs.Api.Core.Repositories;
using Discogs.Api.Infrastructure.Repositories.Helpers;

namespace Discogs.Api.Infrastructure.Repositories
{
    public class DiscogsRepository : IDiscogsRepository
    {
        private static readonly HttpClient Client = new();

        static DiscogsRepository()
        {
            // TODO get api url from configuration
            Client.BaseAddress = new Uri("https://api.discogs.com/");
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Client.DefaultRequestHeaders.Add("User-Agent", "Discogs.API/1.0");
        }

        public async Task<Collection> GetCollectionAsync(SearchCriteria criteria)
        {
            Collection collection = null;

            var response = await Client.GetAsync(UriHelper.FormatCollectionRequestUri(criteria));

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

            var response = await Client.GetAsync(UriHelper.FormatWantlistRequestUri(criteria));

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                wantlist = JsonConvert.DeserializeObject<Wantlist>(result);
            }

            return wantlist;
        }
    }
}
