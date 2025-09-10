using GeoIPIdentification.Applicaiton.DTOs;

public abstract class IpBaseLocationResponse { }

public class IpBaseLocationSuccess : IpBaseLocationResponse
{
    public IpBaseLocationResult Data { get; set; } = default!;
}

public class IpBaseLocationError : IpBaseLocationResponse
{
    public string ErrorMessage { get; set; } = default!;
}
