using Microsoft.AspNetCore.Mvc;
using Discogs.Api.Data;
using System.Linq;
using Discogs.Api.ViewModels;
using Discogs.Api.Mappers;

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
        public IActionResult Get()
        {
            var collection = _repository.GetCollection();

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