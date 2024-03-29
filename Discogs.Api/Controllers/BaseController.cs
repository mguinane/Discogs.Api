﻿using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Discogs.Api.Controllers;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Route("api/[controller]")]
public abstract class BaseController : ControllerBase
{

}
