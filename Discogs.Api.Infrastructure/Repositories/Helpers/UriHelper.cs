using Discogs.Api.Core.Models;
using System.Text;

namespace Discogs.Api.Infrastructure.Repositories.Helpers;

public static class UriHelper
{
    public static string FormatCollectionRequestUri(SearchCriteria criteria)
    {
        StringBuilder requestUri = new();

        // example - users/marcusg/collection/folders/0/releases?sort=artist&page=1&per_page=25

        requestUri.Append($"users/{criteria.Username.ToLower()}/collection/folders/");

        if (criteria.FolderId > 0)
            requestUri.Append($"{criteria.FolderId}/");
        else
            requestUri.Append("0/");

        requestUri.Append("releases?");

        if (criteria.Page > 0)
            requestUri.Append($"page={criteria.Page}");

        if (criteria.PageSize > 0)
            requestUri.Append($"&per_page={criteria.PageSize}");

        if (!string.IsNullOrWhiteSpace(criteria.SortBy))
            requestUri.Append($"&sort={criteria.SortBy}");

        return requestUri.ToString();
    }

    public static string FormatWantlistRequestUri(SearchCriteria criteria)
    {
        StringBuilder requestUri = new();

        // example - users/marcusg/wants?page=1&per_page=25

        requestUri.Append($"users/{criteria.Username.ToLower()}/wants?");

        if (criteria.Page > 0)
            requestUri.Append($"page={criteria.Page}");

        if (criteria.PageSize > 0)
            requestUri.Append($"&per_page={criteria.PageSize}");

        return requestUri.ToString();
    }
}
