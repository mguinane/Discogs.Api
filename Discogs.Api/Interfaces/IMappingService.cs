using Discogs.Api.Core.Models;
using Discogs.Api.Models;

namespace Discogs.Api.Interfaces
{
    public interface IMappingService
    {
        SearchCriteria MapSearchCriteria(SearchCriteriaDTO searchCriteria);
        DiscogsDTO MapCollection(Collection collection);
        DiscogsDTO MapWantlist(Wantlist wantlist);
    }
}
