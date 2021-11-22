using Discogs.Api.Filters;
using System.ComponentModel.DataAnnotations;

namespace Discogs.Api.Models;

public class SearchCriteriaDTO
{
    public SearchCriteriaDTO()
    {
        FolderId = 0;
        SortBy = SortType.artist.ToString();
        Page = 1;
        PageSize = 25;
    }

    public string Username { get; set; }

    public int? FolderId { get; set; }

    [ValidValuesFromEnum(typeof(SortType))]
    public string SortBy { get; set; }

    public int Page { get; set; }
    public int PageSize { get; set; }
}
