namespace Discogs.Api.Core.Models
{
    public class Wantlist
    {
        public Want[] wants { get; set; }
        public Pagination pagination { get; set; }
    }
}
