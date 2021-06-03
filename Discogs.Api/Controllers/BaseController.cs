using Discogs.Api.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Discogs.Api.Controllers
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/[controller]")]
    [ValidateModel]
    public abstract class BaseController : ControllerBase
    {
        
    }
}
