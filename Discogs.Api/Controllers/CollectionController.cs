using Discogs.Api.Core.Repositories;
using Discogs.Api.Core.Services.Logging;
using Discogs.Api.Interfaces;
using Discogs.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Discogs.Api.Controllers;

public class CollectionController : BaseController
{
    private readonly IDiscogsRepository _repository;
    private readonly ILoggerAdapter<CollectionController> _logger;
    private readonly IMappingService _mappingService;

    public CollectionController(IDiscogsRepository repository, ILoggerAdapter<CollectionController> logger,
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
            var collection = await _repository.GetCollectionAsync(_mappingService.MapSearchCriteria(criteria));

            if (collection == null) return NotFound("No Collection data found for specified criteria.");

            // TODO - return urls in pagination ...
            // page=1 - last & next
            // page=end - first & prev
            // all other pages - first, last, prev, next

            var result = _mappingService.MapCollection(collection);

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Exception occured while getting Collection: {ex}");
        }
        return BadRequest();
    }
}
