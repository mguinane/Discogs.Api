namespace Discogs.Api.Core.Models
{
    public class Collection
    {
        public Pagination pagination { get; set; }
        public Release[] releases { get; set; }
    }
}
