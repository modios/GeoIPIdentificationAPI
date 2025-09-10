using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoIPIdentification.Applicaiton.DTOs
{
    public class IpBaseApiResponse
    {
        public IpBaseDataDto Data { get; set; } = default!;
    }

    public class IpBaseDataDto
    {
        public string Ip { get; set; } = default!;
        public IpBaseLocationDto Location { get; set; } = default!;
        public IpBaseTimezoneDto Timezone { get; set; } = default!;
    }

    public class IpBaseLocationDto
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public IpBaseCountryDto Country { get; set; } = default!;
    }

    public class IpBaseCountryDto
    {
        public string Alpha2 { get; set; } = default!;
        public string Name { get; set; } = default!;
    }

    public class IpBaseTimezoneDto
    {
        public string Id { get; set; } = default!;
    }

}
