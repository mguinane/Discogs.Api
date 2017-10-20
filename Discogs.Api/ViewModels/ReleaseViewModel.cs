using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discogs.Api.ViewModels
{
    public class ReleaseViewModel
    {
        public string Artist { get; set; }
        public string Title { get; set; }
        public string Label { get; set; }
        public string Format { get; set; }
        public string FormatDetail { get; set; }
        public string ImageUrl { get; set; }
        public int Year { get; set; }
    }
}
