using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discogs.Api.Models
{
    public class Collection
    {
        public Pagination pagination { get; set; }
        public Release[] releases { get; set; }
    }
}
