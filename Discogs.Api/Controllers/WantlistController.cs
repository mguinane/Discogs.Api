using Microsoft.AspNetCore.Mvc;
using Discogs.Api.Data;
using System.Linq;
using Discogs.Api.ViewModels;
using Discogs.Api.Mappers;

namespace Discogs.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/wantlist")]
    public class WantlistController : Controller
    {
        private readonly IDiscogsRepository _repository;

        public WantlistController(IDiscogsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var wantlist = _repository.GetWantlist();

            var results = wantlist.wants.Select(r => new ReleaseViewModel
            {
                Artist = ReleaseMapper.MapArtistDescription(r.basic_information.artists),
                Label = ReleaseMapper.MapLabelDescription(r.basic_information.labels),
                Format = r.basic_information.formats.FirstOrDefault()?.name,
                FormatDetail = ReleaseMapper.MapFormatDescription(r.basic_information.formats),
                Title = r.basic_information.title,
                // TODO how to get image for wantlist release?
                Year = r.basic_information.year
            });

            return Ok(results);
        }
    }
}