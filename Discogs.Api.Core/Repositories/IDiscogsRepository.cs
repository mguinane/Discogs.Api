using Discogs.Api.Core.Models;
using System.Threading.Tasks;

namespace Discogs.Api.Core.Repositories
{
    public interface IDiscogsRepository
    {
        Task<Collection> GetCollectionAsync(SearchCriteria criteria);
        Task<Wantlist> GetWantlistAsync(SearchCriteria criteria);
    }
}
