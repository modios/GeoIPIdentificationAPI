using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoIPIdentification.Domain.Entities
{
    public class GeoIpResult
    {
        public int Id { get; set; }
        public Guid BatchId { get; set; }
        public string IpAddress { get; set; } = default!; 
        public string CountryCode { get; set; } = default!;
        public string CountryName { get; set; } = default!;
        public string TimeZone { get; set; } = default!;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool IsCompleted { get; set; }

        public GeoIpBatch Batch { get; set; } = default!;
    }

}
