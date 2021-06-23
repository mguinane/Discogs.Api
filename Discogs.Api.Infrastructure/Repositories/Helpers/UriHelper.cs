using Discogs.Api.Core.Models;
using System.Text;

namespace Discogs.Api.Infrastructure.Repositories.Helpers
{
    public static class UriHelper
    {
        public static string FormatCollectionRequestUri(SearchCriteria criteria)
        {
            var requestUri = new StringBuilder();

            // example - users/marcusg/collection/folders/0/releases?sort=artist&page=1&per_page=25

            requestUri.Append("users/");

            if (!string.IsNullOrWhiteSpace(criteria.Username))
                requestUri.Append(criteria.Username.ToLower() + "/");
            else
                requestUri.Append("marcusg/");

            requestUri.Append("collection/folders/");

            if (criteria.FolderId > 0)
                requestUri.Append(criteria.FolderId.ToString() + "/");
            else
                requestUri.Append("0/");

            requestUri.Append("releases?");

            if (criteria.Page > 0)
                requestUri.Append("page=" + criteria.Page.ToString());
            else
                requestUri.Append("page=1");

            if (criteria.PageSize > 0)           
                requestUri.Append("&per_page=" + criteria.PageSize.ToString());
            else
                requestUri.Append("&per_page=25");

            if (!string.IsNullOrWhiteSpace(criteria.SortBy))
                requestUri.Append("&sort=" + criteria.SortBy);

            return requestUri.ToString();
        }

        public static string FormatWantlistRequestUri(SearchCriteria criteria)
        {
            var requestUri = new StringBuilder();

            // example - users/marcusg/wants?page=1&per_page=25

            requestUri.Append("users/");

            if (!string.IsNullOrWhiteSpace(criteria.Username.ToLower()))
                requestUri.Append(criteria.Username + "/");
            else
                requestUri.Append("marcusg/");

            requestUri.Append("wants?");

            if (criteria.Page > 0)
                requestUri.Append("page=" + criteria.Page.ToString());
            else
                requestUri.Append("page=1");

            if (criteria.PageSize > 0)
                requestUri.Append("&per_page=" + criteria.PageSize.ToString());
            else
                requestUri.Append("&per_page=25");

            return requestUri.ToString();
        }
    }
}
