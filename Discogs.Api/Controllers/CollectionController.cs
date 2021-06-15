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

namespace Discogs.Api.Controllers
{
    public class CollectionController : BaseController
    {
        private readonly IDiscogsRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CollectionController> _logger;

        public CollectionController(IDiscogsRepository repository, IMapper mapper, ILogger<CollectionController> logger)
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
                var collection = await _repository.GetCollectionAsync(_mapper.Map<SearchCriteria>(criteria));

                if (collection == null) return NotFound("No Collection data found for specified criteria.");

                var results = collection.releases.Select(r => new ReleaseDTO
                {
                    Artist = r.basic_information.artists.MapDescription(),
                    Label = r.basic_information.labels.MapDescription(),
                    Format = r.basic_information.formats.FirstOrDefault()?.name,
                    FormatDetail = r.basic_information.formats.MapDescription(),
                    Title = r.basic_information.title,
                    ImageUrl = r.basic_information.cover_image,
                    Year = r.basic_information.year
                }).ToList();

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