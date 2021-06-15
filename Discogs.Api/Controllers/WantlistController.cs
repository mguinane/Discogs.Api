using AutoMapper;
using Discogs.Api.Core.Models;
using Discogs.Api.Core.Models.Extensions;
using Discogs.Api.Core.Repositories;
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
        private readonly IMapper _mapper;
        private readonly ILogger<WantlistController> _logger;

        public WantlistController(IDiscogsRepository repository, IMapper mapper, ILogger<WantlistController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] SearchCriteriaDTO criteria)
        {
            try
            {
                var wantlist = await _repository.GetWantlistAsync(_mapper.Map<SearchCriteria>(criteria));

                if (wantlist == null) return NotFound("No Wantlist data found for specified criteria.");

                var results = wantlist.wants.Select(r => new ReleaseDTO
                {
                    Artist = r.basic_information.artists.MapDescription(),
                    Label = r.basic_information.labels.MapDescription(),
                    Format = r.basic_information.formats.FirstOrDefault()?.name,
                    FormatDetail = r.basic_information.formats.MapDescription(),
                    Title = r.basic_information.title,
                    // TODO how to get image for wantlist release?
                    ImageUrl = "img",
                    Year = r.basic_information.year
                }).ToList();

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