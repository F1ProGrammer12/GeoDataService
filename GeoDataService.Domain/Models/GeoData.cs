using System.ComponentModel.DataAnnotations;

namespace GeoDataService.Domain.Models;

public class GeoData
{
    [Required]
    [Range(-180, 180)]
    public double Longitude { get; set; }

    [Required]
    [Range(-90, 90)]
    public double Latitude { get; set; }
}