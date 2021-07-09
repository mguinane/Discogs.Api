using AutoMapper;
using Discogs.Api.Core.Models;
using Discogs.Api.Core.Models.Extensions;
using Discogs.Api.Interfaces;
using Discogs.Api.Models;
using System.Linq;

namespace Discogs.Api.Services
{
    public class MappingService : IMappingService
    {
        private readonly IMapper _mapper;

        public MappingService(IMapper mapper) => _mapper = mapper;

        public SearchCriteria MapSearchCriteria(SearchCriteriaDTO searchCriteria) 
            => _mapper.Map<SearchCriteria>(searchCriteria);

        public DiscogsDTO MapCollection(Collection collection)
        {
            return new DiscogsDTO()
            {
                Pagination = MapPagination(collection.pagination),
                Releases = collection.releases.Select(r => MapCollectionRelease(r)).ToList()
            };
        }

        public DiscogsDTO MapWantlist(Wantlist wantlist)
        {
            return new DiscogsDTO()
            {
                Pagination = MapPagination(wantlist.pagination),
                Releases = wantlist.wants.Select(r => MapWantlistRelease(r)).ToList()
            };
        }

        private PaginationDTO MapPagination(Pagination pagination) 
            => _mapper.Map<PaginationDTO>(pagination);

        private ReleaseDTO MapCollectionRelease(Release release)
        {
            return new ReleaseDTO
            {
                Artist = release.basic_information.artists.MapDescription(),
                Label = release.basic_information.labels.MapDescription(),
                Format = release.basic_information.formats.FirstOrDefault()?.name,
                FormatDetail = release.basic_information.formats.MapDescription(),
                Title = release.basic_information.title,
                ImageUrl = release.basic_information.cover_image,
                Year = release.basic_information.year
            };
        }

        private ReleaseDTO MapWantlistRelease(Want want)
        {
            return new ReleaseDTO
            {
                Artist = want.basic_information.artists.MapDescription(),
                Label = want.basic_information.labels.MapDescription(),
                Format = want.basic_information.formats.FirstOrDefault()?.name,
                FormatDetail = want.basic_information.formats.MapDescription(),
                Title = want.basic_information.title,
                // TODO how to get image for wantlist release?
                ImageUrl = "img",
                Year = want.basic_information.year
            };
        }
    }
}
