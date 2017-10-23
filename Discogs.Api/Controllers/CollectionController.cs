using Microsoft.AspNetCore.Mvc;
using Discogs.Api.Data;
using System.Linq;
using System.Threading.Tasks;
using Discogs.Api.ViewModels;
using Discogs.Api.Helpers;
using Discogs.Api.Models;

namespace Discogs.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/collection")]
    public class CollectionController : Controller
    {
        private readonly IDiscogsRepository _repository;

        public CollectionController(IDiscogsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] SearchCriteria criteria)
        {
            var collection = await _repository.GetCollection(criteria);

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
    }
}