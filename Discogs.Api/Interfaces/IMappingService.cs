using Discogs.Api.Core.Models;
using Discogs.Api.Models;

namespace Discogs.Api.Interfaces
{
    public interface IMappingService
    {
        SearchCriteria MapSearchCriteriaDTO(SearchCriteriaDTO searchCriteria);
        PaginationDTO MapPagination(Pagination pagination);
        ReleaseDTO MapCollectionRelease(Release release);
        ReleaseDTO MapWantlistRelease(Want want);
    }
}
