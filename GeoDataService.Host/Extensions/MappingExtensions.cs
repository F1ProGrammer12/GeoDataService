using GeoDataService.Domain.Models;
using GeoDataService.Host.Dto;
using System.Net;

namespace GeoDataService.Host.Extensions;

public static class MappingExtensions
{
    public static Address ToAddress(this GetLongitudeAndLatitudeRequest request) => new()
    {
        Country = request.Country,
        City = request.City,
        Street = request.Street,
        BuildingNumber = request.BuildingNumber
    };

    public static GeoData ToGeoData(this GetTenNearestAddressesRequest request) => new()
    {
        Latitude = request.Latitude,
        Longitude = request.Longitude,
    };
}