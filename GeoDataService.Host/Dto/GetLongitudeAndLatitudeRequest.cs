namespace GeoDataService.Host.Dto;

public class GetLongitudeAndLatitudeRequest
{
    public string Country { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string BuildingNumber { get; set; } = null!;
}