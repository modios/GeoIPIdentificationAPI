using System.ComponentModel.DataAnnotations;

namespace GeoIPIdentification.Applicaiton.DTOs
{
    public class GeoIpBatchRequest
    {
        [Required]
        public List<string> IpAddresses { get; set; } = new();
    }
}
