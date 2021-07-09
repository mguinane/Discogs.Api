using AutoMapper;
using Discogs.Api.Controllers;
using Discogs.Api.Core.Models;
using Discogs.Api.Core.Repositories;
using Discogs.Api.Core.Services.Logging;
using Discogs.Api.Interfaces;
using Discogs.Api.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Discogs.Api.Tests.Controllers
{
    public class CollectionControllerTest
    {
        private CollectionController _collectionController;
        private Mock<IDiscogsRepository> _mockDiscogsRepository;
        private Mock<ILoggerAdapter<CollectionController>> _mockLogger;
        private Mock<IMappingService> _mockMappingService;
        private static IMapper _mapper;

        public CollectionControllerTest()
        {
            _mockDiscogsRepository = new();
            _mockLogger = new();
            _mockMappingService = new();
            _collectionController = new(_mockDiscogsRepository.Object, _mockLogger.Object, _mockMappingService.Object);

            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MapperProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }

        [Fact]
        public async Task Get_AnySearchCriteria_ReturnOkResult()
        {
            _mockDiscogsRepository.Setup(r => r.GetCollectionAsync(It.IsAny<SearchCriteria>())).ReturnsAsync(() => new());

            var result = await _collectionController.Get(new SearchCriteriaDTO()) as ObjectResult;

            var actionResult = result.Should().BeOfType<OkObjectResult>().Subject;
        }

        [Fact]
        public async Task Get_AnySearchCriteriaAndEmptyRepository_ReturnNotFound()
        {
            _mockDiscogsRepository.Setup(r => r.GetCollectionAsync(It.IsAny<SearchCriteria>())).ReturnsAsync(() => null);

            var result = await _collectionController.Get(new SearchCriteriaDTO()) as ObjectResult;

            var actionResult = result.Should().BeOfType<NotFoundObjectResult>().Subject;
        }
    }
}
