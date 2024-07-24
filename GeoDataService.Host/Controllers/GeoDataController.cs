using GeoDataService.Host.Dto;
using GeoDataService.Host.Extensions;
using GeoDataService.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GeoDataService.Host.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GeoDataController(IGeoDataService geoDataService) : ControllerBase
{
    [HttpPost("get_10_nearest_addresses")]
    public async Task<ActionResult> GetTenNearestAddresses([FromBody] GetTenNearestAddressesRequest request)
    {
        var addresses = await geoDataService.GetTenNearestAddresses(request.ToGeoData());
        return Ok(addresses);
    }

    [HttpPost("get_longitude_and_latitude")]
    public async Task<ActionResult> GetLongitudeAndLatitude([FromBody] GetLongitudeAndLatitudeRequest request)
    {
        var geoData = await geoDataService.GetLongitudeAndLattitude(request.ToAddress());
        return Ok(geoData);
    }
}