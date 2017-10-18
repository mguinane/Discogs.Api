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
    [Route("api/wantlist")]
    public class WantlistController : Controller
    {
        private IDiscogsRepository _repository;

        public WantlistController(IDiscogsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var wantlist = _repository.GetWantlist();

            return Ok(wantlist);
        }
    }
}