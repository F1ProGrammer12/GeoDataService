using System.ComponentModel.DataAnnotations;

namespace GeoDataService.Domain.Models;

public class Address
{
    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string Country { get; set; } = null!;

    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string City { get; set; } = null!;

    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string Street { get; set; } = null!;

    [Required]
    [StringLength(10, MinimumLength = 1)]
    public string BuildingNumber { get; set; } = null!;
}