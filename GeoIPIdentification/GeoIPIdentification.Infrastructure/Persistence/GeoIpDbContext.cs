using GeoIPIdentification.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GeoIPIdentification.Infrastructure.Persistence
{
    public class GeoIpDbContext : DbContext
    {
        public GeoIpDbContext(DbContextOptions<GeoIpDbContext> options) : base(options) { }

        public DbSet<GeoIpBatch> GeoIpBatches { get; set; }
        public DbSet<GeoIpResult> GeoIpResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GeoIpBatch>()
                .HasMany(b => b.Results)
                .WithOne(r => r.Batch)
                .HasForeignKey(r => r.BatchId);
        }
    }
}
