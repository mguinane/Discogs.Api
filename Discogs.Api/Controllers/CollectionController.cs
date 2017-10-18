using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Discogs.Api.Data;

namespace Discogs.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/collection")]
    public class CollectionController : Controller
    {
        private IDiscogsRepository _repository;

        public CollectionController(IDiscogsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var collection = _repository.GetCollection();

            return Ok(collection);
        }
    }
}