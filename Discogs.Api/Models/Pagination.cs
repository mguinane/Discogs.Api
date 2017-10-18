using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discogs.Api.Models
{
    public class Pagination
    {
        public int per_page { get; set; }
        public int items { get; set; }
        public int page { get; set; }
        public Urls urls { get; set; }
        public int pages { get; set; }
    }
}
