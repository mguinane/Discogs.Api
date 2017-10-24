using Discogs.Api.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discogs.Api.Models
{
    public class SearchCriteria
    {
        [MinLength(5)]
        [MaxLength(100)]
        public string Username { get; set; }

        public int? FolderId { get; set; }

        [MinLength(4)]
        [MaxLength(6)]
        [ValidValuesFromEnum(typeof(SortType))]
        public string SortBy { get; set; }

        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
