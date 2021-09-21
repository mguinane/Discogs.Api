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

namespace Discogs.Api.Tests.Controllers
{
    public class WantlistControllerTests
    {
        private WantlistController _wantlistController;
        private Mock<IDiscogsRepository> _mockDiscogsRepository;
        private Mock<ILoggerAdapter<WantlistController>> _mockLogger;
        private Mock<IMappingService> _mockMappingService;

        private readonly Wantlist _wantlist;
        private readonly DiscogsDTO _wantlistDTO;

        public WantlistControllerTests()
        {
            _mockDiscogsRepository = new();
            _mockLogger = new();
            _mockMappingService = new();
            _wantlistController = new(_mockDiscogsRepository.Object, _mockLogger.Object, _mockMappingService.Object);

            _wantlist = FakeDataHelper.DeserializeFromJsonFile<Wantlist>("wantlist");
            _wantlistDTO = FakeDataHelper.DeserializeFromJsonFile<DiscogsDTO>("wantlistDTO");
        }

        [Fact]
        public async Task Get_AnySearchCriteria_ReturnOkObjectResult()
        {
            _mockDiscogsRepository.Setup(r => r.GetWantlistAsync(It.IsAny<SearchCriteria>())).ReturnsAsync(() => new());

            var result = await _wantlistController.Get(new SearchCriteriaDTO()) as ObjectResult;

            var actionResult = result.Should().BeOfType<OkObjectResult>().Subject;
        }

        [Fact]
        public async Task Get_AnySearchCriteriaAndEmptyRepository_ReturnNotFoundObjectResult()
        {
            _mockDiscogsRepository.Setup(r => r.GetWantlistAsync(It.IsAny<SearchCriteria>())).ReturnsAsync(() => null);

            var result = await _wantlistController.Get(new SearchCriteriaDTO()) as ObjectResult;

            var actionResult = result.Should().BeOfType<NotFoundObjectResult>().Subject;
        }

        [Fact]
        public async Task Get_AnySearchCriteriaAndNonEmptyRepository_ReturnDiscogsDTO()
        {
            _mockDiscogsRepository.Setup(r => r.GetWantlistAsync(It.IsAny<SearchCriteria>())).ReturnsAsync(() => _wantlist);
            _mockMappingService.Setup(s => s.MapWantlist(It.IsAny<Wantlist>())).Returns(() => _wantlistDTO);

            var result = await _wantlistController.Get(new SearchCriteriaDTO()) as ObjectResult;

            var discogsDTO = result.Value.Should().BeOfType<DiscogsDTO>().Subject;
        }

        [Fact]
        public async Task Get_RepositoryThrowsException_ReturnBadRequestResult()
        {
            _mockDiscogsRepository.Setup(r => r.GetWantlistAsync(It.IsAny<SearchCriteria>())).Throws<Exception>();

            var result = await _wantlistController.Get(new SearchCriteriaDTO());

            var actionResult = result.Should().BeOfType<BadRequestResult>().Subject;
        }

        [Fact]
        public async Task Get_ExceptionThrown_ErrorLogged()
        {
            _mockDiscogsRepository.Setup(r => r.GetWantlistAsync(It.IsAny<SearchCriteria>())).Throws<Exception>();

            var result = await _wantlistController.Get(new SearchCriteriaDTO());

            _mockLogger.Verify(l => l.LogError(It.IsAny<string>()));
        }
    }
}
