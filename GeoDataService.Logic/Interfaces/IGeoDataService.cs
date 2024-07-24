using GeoDataService.Domain.Models;

namespace GeoDataService.Logic.Interfaces;

public interface IGeoDataService
{
    Task<IEnumerable<Address>> GetTenNearestAddresses(GeoData geoData);
    Task<GeoData> GetLongitudeAndLattitude(Address address);
}