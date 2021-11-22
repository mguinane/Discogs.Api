using Discogs.Api.Core.Repositories;
using Discogs.Api.Core.Services.Logging;
using Discogs.Api.Interfaces;
using Discogs.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Discogs.Api.Controllers;

public class WantlistController : BaseController
{
    private readonly IDiscogsRepository _repository;
    private readonly ILoggerAdapter<WantlistController> _logger;
    private readonly IMappingService _mappingService;

    public WantlistController(IDiscogsRepository repository, ILoggerAdapter<WantlistController> logger,
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
            var wantlist = await _repository.GetWantlistAsync(_mappingService.MapSearchCriteria(criteria));

            if (wantlist == null) return NotFound("No Wantlist data found for specified criteria.");

            var result = _mappingService.MapWantlist(wantlist);

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Exception occured while getting Wantlist: {ex}");
        }
        return BadRequest();
    }
}
