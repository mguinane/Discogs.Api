namespace Discogs.Api.Models
{
    public class Collection
    {
        public Pagination pagination { get; set; }
        public Release[] releases { get; set; }
    }
}
