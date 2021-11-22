using Discogs.Api.Core.Models;
using Discogs.Api.Infrastructure.Repositories.Helpers;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Discogs.Api.Tests.Repositories.Helpers;

public class UriHelperTests
{
    [Fact]
    public void FormatCollectionRequestUri_EmptySearchCriteria_SetDefaultParameters()
    {
        SearchCriteria searchCriteria = new() { Username = "marcusg" };

        var requestUri = UriHelper.FormatCollectionRequestUri(searchCriteria);

        using (new AssertionScope())
        {
            requestUri.Should().Contain("/folders/0/"); //default folder value
            requestUri.Should().EndWith("releases?"); //no params
        }
    }

    [Fact]
    public void FormatCollectionRequestUri_SearchCriteria_SetParameters()
    {
        SearchCriteria searchCriteria = new() { Username = "marcusg", FolderId = 5, Page = 15, PageSize = 50, SortBy = "artist" };

        var requestUri = UriHelper.FormatCollectionRequestUri(searchCriteria);

        using (new AssertionScope())
        {
            requestUri.Should().Contain($"/folders/{searchCriteria.FolderId}/");
            requestUri.Should().Contain($"page={searchCriteria.Page}");
            requestUri.Should().Contain($"&per_page={searchCriteria.PageSize}");
            requestUri.Should().Contain($"&sort={searchCriteria.SortBy}");
        }
    }

    [Fact]
    public void FormatWantlistRequestUri_EmptySearchCriteria_SetDefaultParameters()
    {
        SearchCriteria searchCriteria = new() { Username = "marcusg" };

        var requestUri = UriHelper.FormatWantlistRequestUri(searchCriteria);

        requestUri.Should().EndWith("wants?"); //no params
    }

    [Fact]
    public void FormatWantlistRequestUri_SearchCriteria_SetParameters()
    {
        SearchCriteria searchCriteria = new() { Username = "marcusg", Page = 15, PageSize = 50 };

        var requestUri = UriHelper.FormatWantlistRequestUri(searchCriteria);

        using (new AssertionScope())
        {
            requestUri.Should().Contain($"page={searchCriteria.Page}");
            requestUri.Should().Contain($"&per_page={searchCriteria.PageSize}");
        }
    }
}
