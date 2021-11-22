using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discogs.Api.Models;

public class PaginationDTO
{
    public int Page { get; set; }
    public int Pages { get; set; }
    public int PerPage { get; set; }
    public int Items { get; set; }
    public string NextUrl { get; set; }
    public string LastUrl { get; set; }
}
