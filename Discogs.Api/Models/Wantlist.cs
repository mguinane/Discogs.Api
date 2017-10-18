using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discogs.Api.Models
{

    public class Wantlist
    {
        public Want[] wants { get; set; }
        public Pagination pagination { get; set; }
    }
}
