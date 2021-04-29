using System;
using Microsoft.AspNetCore.Mvc;
using Discogs.Api.Data;
using System.Linq;
using Discogs.Api.ViewModels;
using Discogs.Api.Helpers;
using System.Threading.Tasks;
using Discogs.Api.Filters;
using Discogs.Api.Models;
using Microsoft.Extensions.Logging;

namespace Discogs.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ValidateModel]
    public class WantlistController : ControllerBase
    {
        private readonly IDiscogsRepository _repository;
        private readonly ILogger<WantlistController> _logger;

        public WantlistController(IDiscogsRepository repository, ILogger<WantlistController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] SearchCriteria criteria)
        {
            try
            {
                var wantlist = await _repository.GetWantlistAsync(criteria);

                if (wantlist == null) return NotFound("No Wantlist data found for specified criteria.");

                var results = wantlist.wants.Select(r => new ReleaseViewModel
                {
                    Artist = ReleaseMapper.MapArtistDescription(r.basic_information.artists),
                    Label = ReleaseMapper.MapLabelDescription(r.basic_information.labels),
                    Format = r.basic_information.formats.FirstOrDefault()?.name,
                    FormatDetail = ReleaseMapper.MapFormatDescription(r.basic_information.formats),
                    Title = r.basic_information.title,
                    // TODO how to get image for wantlist release?
                    ImageUrl = "img",
                    Year = r.basic_information.year
                });

                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured while getting Wantlist: {ex}");
            }
            return BadRequest();
        }
    }
}