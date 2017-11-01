namespace Discogs.Api.Models
{
    public class Basic_Information
    {
        public Label[] labels { get; set; }
        public Format[] formats { get; set; }
        public Artist[] artists { get; set; }
        public string thumb { get; set; }
        public string title { get; set; }
        public string cover_image { get; set; }
        public string resource_url { get; set; }
        public int year { get; set; }
        public int id { get; set; }
    }
}
