using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discogs.Api.Models
{
    public class Artist
    {
        public string join { get; set; }
        public string name { get; set; }
        public string anv { get; set; }
        public string tracks { get; set; }
        public string role { get; set; }
        public string resource_url { get; set; }
        public int id { get; set; }
    }
}
