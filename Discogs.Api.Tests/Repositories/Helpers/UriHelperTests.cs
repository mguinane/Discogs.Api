﻿using Discogs.Api.Core.Models;
using Discogs.Api.Infrastructure.Repositories.Helpers;
using FluentAssertions;
using Xunit;

namespace Discogs.Api.Tests.Repositories.Helpers
{
    public class UriHelperTests
    {
        [Fact]
        public void FormatCollectionRequestUri_EmptySearchCriteria_SetDefaultParameters()
        {
            SearchCriteria searchCriteria = new();

            var requestUri = UriHelper.FormatCollectionRequestUri(searchCriteria);

            requestUri.Should().Contain("/folders/0/"); //default folder value
            requestUri.Should().Contain("page=1"); //default page value
            requestUri.Should().Contain("&per_page=25"); //default per page value
            requestUri.Should().NotContain("&sort="); //no sort value
        }

        [Fact]
        public void FormatCollectionRequestUri_SearchCriteria_SetParameters()
        {
            SearchCriteria searchCriteria = new() { FolderId = 5, Page=15, PageSize=50, SortBy="artist" };

            var requestUri = UriHelper.FormatCollectionRequestUri(searchCriteria);

            requestUri.Should().Contain($"/folders/{searchCriteria.FolderId}/");
            requestUri.Should().Contain($"page={searchCriteria.Page}");
            requestUri.Should().Contain($"&per_page={searchCriteria.PageSize}");
            requestUri.Should().Contain($"&sort={searchCriteria.SortBy}");
        }

        [Fact]
        public void FormatWantlistRequestUri_EmptySearchCriteria_SetDefaultParameters()
        {
            SearchCriteria searchCriteria = new();

            var requestUri = UriHelper.FormatWantlistRequestUri(searchCriteria);

            requestUri.Should().Contain("page=1"); //default page value
            requestUri.Should().Contain("&per_page=25"); //default per page value
        }

        [Fact]
        public void FormatWantlistRequestUri_SearchCriteria_SetParameters()
        {
            SearchCriteria searchCriteria = new() { Page = 15, PageSize = 50 };

            var requestUri = UriHelper.FormatWantlistRequestUri(searchCriteria);

            requestUri.Should().Contain($"page={searchCriteria.Page}");
            requestUri.Should().Contain($"&per_page={searchCriteria.PageSize}");
        }
    }
}
