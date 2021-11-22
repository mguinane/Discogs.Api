using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discogs.Api.Models;

public class DiscogsDTO
{
    public PaginationDTO Pagination { get; set; }

    public IEnumerable<ReleaseDTO> Releases { get; set; }
}
