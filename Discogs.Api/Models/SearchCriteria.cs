using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discogs.Api.Models
{
    public class SearchCriteria
    {
        public string Username { get; set; }
        public int? FolderId { get; set; }
        public string SortBy { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
