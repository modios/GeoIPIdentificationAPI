using GeoIPIdentification.Applicaiton.DTOs;

namespace GeoIPIdentification.Applicaiton.Interfaces
{
    public interface IIpBaseService
    {
        Task<IpBaseLocationResponse> GetLocationAsync(string ip);
    }
}
