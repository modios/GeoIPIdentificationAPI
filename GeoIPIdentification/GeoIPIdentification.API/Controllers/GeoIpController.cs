using GeoIPIdentification.Applicaiton.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GeoIPIdentification.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeoIpController : ControllerBase
    {
        private readonly IIpBaseService _ipBaseService;
        private readonly ILogger<GeoIpController> _logger;

        public GeoIpController(IIpBaseService ipBaseService, ILogger<GeoIpController> logger)
        {
            _ipBaseService = ipBaseService;
            _logger = logger;
        }

        [HttpGet("{ip}")]
        public async Task<IActionResult> GetIpLocation(string ip)
        {
            try
            {
                if (!IPAddress.TryParse(ip, out _))
                {
                    _logger.LogWarning("Invalid IP address format: {Ip}", ip);
                    return BadRequest("Invalid IP address format.");
                }

                var result = await _ipBaseService.GetLocationAsync(ip);
                if (result == null)
                {
                    _logger.LogInformation("No location data found for IP: {Ip}", ip);
                    return NotFound("IP information could not be retrieved.");
                }

                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP error while retrieving IP location for {Ip}", ip);
                return StatusCode(503, "External service unavailable.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while retrieving IP location for {Ip}", ip);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
