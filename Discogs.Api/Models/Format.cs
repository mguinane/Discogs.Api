using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discogs.Api.Models
{
    public class Format
    {
        public string[] descriptions { get; set; }
        public string text { get; set; }
        public string name { get; set; }
        public string qty { get; set; }
    }
}
