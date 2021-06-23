using AutoMapper;
using Discogs.Api.Core.Models;
using Discogs.Api.Core.Models.Extensions;
using Discogs.Api.Core.Repositories;
using Discogs.Api.Interfaces;
using Discogs.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Discogs.Api.Controllers
{
    public class WantlistController : BaseController
    {
        private readonly IDiscogsRepository _repository;
        private readonly ILogger<WantlistController> _logger;
        private readonly IMappingService _mappingService;

        public WantlistController(IDiscogsRepository repository, ILogger<WantlistController> logger, 
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
                var wantlist = await _repository.GetWantlistAsync(_mappingService.MapSearchCriteriaDTO(criteria));

                if (wantlist == null) return NotFound("No Wantlist data found for specified criteria.");

                DiscogsDTO result = new()
                {
                    Pagination = _mappingService.MapPagination(wantlist.pagination),
                    Releases = wantlist.wants.Select(r => _mappingService.MapWantlistRelease(r)).ToList()
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured while getting Wantlist: {ex}");
            }
            return BadRequest();
        }
    }
}