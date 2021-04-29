using Discogs.Api.Data;
using Discogs.Api.Filters;
using Discogs.Api.Helpers;
using Discogs.Api.Models;
using Discogs.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Discogs.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ValidateModel]
    public class CollectionController : ControllerBase
    {
        private readonly IDiscogsRepository _repository;
        private readonly ILogger<CollectionController> _logger;

        public CollectionController(IDiscogsRepository repository, ILogger<CollectionController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] SearchCriteria criteria)
        {
            try
            {
                var collection = await _repository.GetCollectionAsync(criteria);

                if (collection == null) return NotFound("No Collection data found for specified criteria.");

                var results = collection.releases.Select(r => new ReleaseViewModel
                {
                    Artist = ReleaseMapper.MapArtistDescription(r.basic_information.artists),
                    Label = ReleaseMapper.MapLabelDescription(r.basic_information.labels),
                    Format = r.basic_information.formats.FirstOrDefault()?.name,
                    FormatDetail = ReleaseMapper.MapFormatDescription(r.basic_information.formats),
                    Title = r.basic_information.title,
                    ImageUrl = r.basic_information.cover_image,
                    Year = r.basic_information.year
                });

                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured while getting Collection: {ex}");
            }
            return BadRequest();
        }
    }
}