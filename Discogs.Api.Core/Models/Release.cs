using System;

namespace Discogs.Api.Core.Models
{
    public class Release
    {
        public int instance_id { get; set; }
        public DateTime date_added { get; set; }
        public Basic_Information basic_information { get; set; }
        public int id { get; set; }
        public int rating { get; set; }
    }
}
