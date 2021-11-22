using AutoMapper;
using Discogs.Api.Core.Models;

namespace Discogs.Api.Models;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<SearchCriteriaDTO, SearchCriteria>();

        CreateMap<Pagination, PaginationDTO>()
            .ForMember(dest => dest.PerPage, opt => opt.MapFrom(src => src.per_page));
    }
}
