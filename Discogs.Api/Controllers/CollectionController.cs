using Discogs.Api.Core.Repositories;
using Discogs.Api.Core.Models;
using Discogs.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Discogs.Api.Core.Models.Extensions;
using Discogs.Api.Interfaces;

namespace Discogs.Api.Controllers
{
    public class CollectionController : BaseController
    {
        private readonly IDiscogsRepository _repository;
        private readonly ILogger<CollectionController> _logger;
        private readonly IMappingService _mappingService;

        public CollectionController(IDiscogsRepository repository, ILogger<CollectionController> logger,
            IMappingService mappingService)
        {
            _repository = repository;
            _logger = logger;
            _mappingService = mappingService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] SearchCriteriaDTO criteria)
        {
            try
            {
                var collection = await _repository.GetCollectionAsync(_mappingService.MapSearchCriteriaDTO(criteria));

                if (collection == null) return NotFound("No Collection data found for specified criteria.");

                // TODO - return next/last urls in pagination ...

                DiscogsDTO result = new()
                {
                    Pagination = _mappingService.MapPagination(collection.pagination),
                    Releases = collection.releases.Select(r => _mappingService.MapCollectionRelease(r)).ToList()
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured while getting Collection: {ex}");
            }
            return BadRequest();
        }
    }
}