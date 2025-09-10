namespace GeoIPIdentification.Domain.Entities
{
    public class GeoIpBatch
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int TotalCount { get; set; }
        public int CompletedCount { get; set; }

        public ICollection<GeoIpResult> Results { get; set; } = new List<GeoIpResult>();
    }
}
