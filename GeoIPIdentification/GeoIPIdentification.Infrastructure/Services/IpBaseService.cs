using GeoIPIdentification.Applicaiton.DTOs;
using GeoIPIdentification.Applicaiton.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace GeoIPIdentification.Infrastructure.Services
{
  public class IpBaseService : IIpBaseService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger<IpBaseService> _logger;

        public IpBaseService(HttpClient httpClient, IConfiguration configuration, ILogger<IpBaseService> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<IpBaseLocationResponse> GetLocationAsync(string ip)
        {
            try
            {
                var apiKey = _configuration["IpBase:ApiKey"];
                var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.ipbase.com/v2/info?ip={ip}");
                request.Headers.Add("apikey", apiKey);

                var response = await _httpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    var error = $"IpBase API returned status code: {response.StatusCode}";
                    _logger.LogWarning(error);
                    return new IpBaseLocationError { ErrorMessage = error };
                }

                var result = await response.Content.ReadFromJsonAsync<IpBaseApiResponse>();

                if (result?.Data?.Location?.Country == null || result.Data.Timezone == null)
                {
                    var error = "IpBase API response missing expected fields.";
                    _logger.LogWarning(error);
                    return new IpBaseLocationError { ErrorMessage = error };
                }

                var location = new IpBaseLocationResult
                {
                    Ip = result.Data.Ip,
                    CountryCode = result.Data.Location.Country.Alpha2,
                    CountryName = result.Data.Location.Country.Name,
                    TimeZone = result.Data.Timezone.Id,
                    Latitude = result.Data.Location.Latitude,
                    Longitude = result.Data.Location.Longitude
                };

                return new IpBaseLocationSuccess { Data = location };
            }
            catch (Exception ex)
            {
                var error = $"Exception occurred: {ex.Message}";
                _logger.LogError(ex, error);
                return new IpBaseLocationError { ErrorMessage = error };
            }
        }
    }

}
