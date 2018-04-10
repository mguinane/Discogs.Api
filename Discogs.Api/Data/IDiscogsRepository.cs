using Discogs.Api.Models;
using System.Threading.Tasks;

namespace Discogs.Api.Data
{
    public interface IDiscogsRepository
    {
        Task<Collection> GetCollectionAsync(SearchCriteria criteria);
        Task<Wantlist> GetWantlistAsync(SearchCriteria criteria);
    }
}
