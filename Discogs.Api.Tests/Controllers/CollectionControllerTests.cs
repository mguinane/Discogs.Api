using AutoMapper;
using Discogs.Api.Controllers;
using Discogs.Api.Core.Models;
using Discogs.Api.Core.Repositories;
using Discogs.Api.Core.Services.Logging;
using Discogs.Api.Interfaces;
using Discogs.Api.Models;
using Discogs.Api.Tests.TestHelpers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Discogs.Api.Tests.Controllers;

public class CollectionControllerTests
{
    private CollectionController _collectionController;
    private Mock<IDiscogsRepository> _mockDiscogsRepository;
    private Mock<ILoggerAdapter<CollectionController>> _mockLogger;
    private Mock<IMappingService> _mockMappingService;

    private readonly Collection _collection;
    private readonly DiscogsDTO _collectionDTO;

    public CollectionControllerTests()
    {
        _mockDiscogsRepository = new();
        _mockLogger = new();
        _mockMappingService = new();
        _collectionController = new(_mockDiscogsRepository.Object, _mockLogger.Object, _mockMappingService.Object);

        _collection = FakeDataHelper.DeserializeFromJsonFile<Collection>("collection");
        _collectionDTO = FakeDataHelper.DeserializeFromJsonFile<DiscogsDTO>("collectionDTO");
    }

    [Fact]
    public async Task Get_AnySearchCriteria_ReturnOkObjectResult()
    {
        _mockDiscogsRepository.Setup(r => r.GetCollectionAsync(It.IsAny<SearchCriteria>())).ReturnsAsync(() => new());

        var result = await _collectionController.Get(new SearchCriteriaDTO()) as ObjectResult;

        var actionResult = result.Should().BeOfType<OkObjectResult>().Subject;
    }

    [Fact]
    public async Task Get_AnySearchCriteriaAndEmptyRepository_ReturnNotFoundObjectResult()
    {
        _mockDiscogsRepository.Setup(r => r.GetCollectionAsync(It.IsAny<SearchCriteria>())).ReturnsAsync(() => null);

        var result = await _collectionController.Get(new SearchCriteriaDTO()) as ObjectResult;

        var actionResult = result.Should().BeOfType<NotFoundObjectResult>().Subject;
    }

    [Fact]
    public async Task Get_AnySearchCriteriaAndNonEmptyRepository_ReturnDiscogsDTO()
    {
        _mockDiscogsRepository.Setup(r => r.GetCollectionAsync(It.IsAny<SearchCriteria>())).ReturnsAsync(() => _collection);
        _mockMappingService.Setup(s => s.MapCollection(It.IsAny<Collection>())).Returns(() => _collectionDTO);

        var result = await _collectionController.Get(new SearchCriteriaDTO()) as ObjectResult;

        var discogsDTO = result.Value.Should().BeOfType<DiscogsDTO>().Subject;
    }

    [Fact]
    public async Task Get_RepositoryThrowsException_ReturnBadRequestResult()
    {
        _mockDiscogsRepository.Setup(r => r.GetCollectionAsync(It.IsAny<SearchCriteria>())).Throws<Exception>();

        var result = await _collectionController.Get(new SearchCriteriaDTO());

        var actionResult = result.Should().BeOfType<BadRequestResult>().Subject;
    }

    [Fact]
    public async Task Get_ExceptionThrown_ErrorLogged()
    {
        _mockDiscogsRepository.Setup(r => r.GetCollectionAsync(It.IsAny<SearchCriteria>())).Throws<Exception>();

        var result = await _collectionController.Get(new SearchCriteriaDTO());

        _mockLogger.Verify(l => l.LogError(It.IsAny<string>()));
    }
}
