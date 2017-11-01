using Discogs.Api.Models;
using System.Threading.Tasks;

namespace Discogs.Api.Data
{
    public interface IDiscogsRepository
    {
        Task<Collection> GetCollection(SearchCriteria criteria);
        Task<Wantlist> GetWantlist(SearchCriteria criteria);
    }
}
