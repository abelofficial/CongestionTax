using System.ComponentModel.DataAnnotations;
using CongestionTaxCalculator.Api.Models;

namespace CongestionTaxCalculator.Api.Dtos;

public class VehicleRequest : IVehicle
{
    [Required]
    public string VehicleType { get; set; }

    public string GetVehicleType() => VehicleType;
}