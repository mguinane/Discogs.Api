using Discogs.Api.Core.Models;
using Discogs.Api.Infrastructure.Repositories;
using Discogs.Api.Tests.TestHelpers;
using FluentAssertions;
using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Discogs.Api.Tests.Repositories;

public class DiscogsRepositoryTests
{
    private DiscogsRepository _discogsRepository;
    private Mock<HttpMessageHandler> _mockHttpMessageHandler;

    private readonly SearchCriteria _searchCriteria;

    public DiscogsRepositoryTests()
    {
        _mockHttpMessageHandler = new();

        _searchCriteria = new() { Username = "marcug", Page = 50, PageSize = 25, SortBy = "artist" };
    }

    [Fact]
    public async Task GetCollection_StatusCodeOK_ReturnCollection()
    {
        Setup(HttpStatusCode.OK, FakeDataHelper.ReadFromJsonFile("collection"));

        var result = await _discogsRepository.GetCollectionAsync(_searchCriteria);

        result.Should().NotBeNull();
    }

    [Fact]
    public async Task GetCollection_StatusCodeNotFound_ReturnNull()
    {
        Setup(HttpStatusCode.NotFound);

        var result = await _discogsRepository.GetCollectionAsync(_searchCriteria);

        result.Should().BeNull();
    }

    [Fact]
    public async Task GetWantlist_StatusCodeOK_ReturnWantlist()
    {
        Setup(HttpStatusCode.OK, FakeDataHelper.ReadFromJsonFile("wantlist"));

        var result = await _discogsRepository.GetWantlistAsync(_searchCriteria);

        result.Should().NotBeNull();
    }

    [Fact]
    public async Task GetWantlist_StatusCodeNotFound_ReturnNull()
    {
        Setup(HttpStatusCode.NotFound);

        var result = await _discogsRepository.GetWantlistAsync(_searchCriteria);

        result.Should().BeNull();
    }

    private void Setup(HttpStatusCode statusCode, string messageContent = "")
    {
        _mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StringContent(messageContent)
            });

        HttpClient httpClient = new(_mockHttpMessageHandler.Object)
        {
            BaseAddress = new Uri("https://api.discogs.com/")
        };

        _discogsRepository = new(httpClient);
    }
}
