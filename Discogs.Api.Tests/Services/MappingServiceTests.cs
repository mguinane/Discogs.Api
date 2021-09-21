﻿using AutoMapper;
using Discogs.Api.Core.Models;
using Discogs.Api.Models;
using Discogs.Api.Services;
using Discogs.Api.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Discogs.Api.Tests.Services
{
    public class MappingServiceTests
    {
        private MappingService _mappingService;
        private static IMapper _mapper;

        private readonly Collection _collection;
        private readonly Wantlist _wantlist;

        public MappingServiceTests()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MapperProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }

            _mappingService = new(_mapper);

            _collection = FakeDataHelper.DeserializeFromJsonFile<Collection>("collection");
            _wantlist = FakeDataHelper.DeserializeFromJsonFile<Wantlist>("wantlist");
        }

        [Fact]
        public void MapSearchCriteria_ValidSearchCriteriaDTO_ReturnsPopulatedSearchCriteria()
        {
            SearchCriteriaDTO searchCriteriaDTO = new() { Username = "marcusg", SortBy = "artist", Page = 1, PageSize = 10 };

            var searchCriteria = _mappingService.MapSearchCriteria(searchCriteriaDTO);

            searchCriteria.Should().BeEquivalentTo(searchCriteriaDTO);
        }

        [Fact]
        public void MapCollection_ValidCollection_ReturnsPopulatedDiscogsDTO()
        {
            var discogsDTO = _mappingService.MapCollection(_collection);

            discogsDTO.Pagination.Pages.Should().Be(_collection.pagination.pages);
            discogsDTO.Releases.Should().HaveCount(c => c == _collection.releases.Length);
        }

        [Fact]
        public void MapWantlist_ValidWantlist_ReturnsPopulatedDiscogsDTO()
        {
            var discogsDTO = _mappingService.MapWantlist(_wantlist);

            discogsDTO.Pagination.Pages.Should().Be(_wantlist.pagination.pages);
            discogsDTO.Releases.Should().HaveCount(c => c == _wantlist.wants.Length);
        }
    }
}