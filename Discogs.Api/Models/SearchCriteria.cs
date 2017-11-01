using Discogs.Api.Filters;
using System.ComponentModel.DataAnnotations;

namespace Discogs.Api.Models
{
    public class SearchCriteria
    {
        [MinLength(5)]
        [MaxLength(100)]
        public string Username { get; set; }

        public int? FolderId { get; set; }

        [ValidValuesFromEnum(typeof(SortType))]
        public string SortBy { get; set; }

        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
