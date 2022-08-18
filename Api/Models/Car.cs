namespace CongestionTaxCalculator.Api.Models;

public class Car : IVehicle
{
    public string VehicleType { get; set; }

    public Car()
    {
        VehicleType = "Car";
    }
    public String GetVehicleType()
    {
        return VehicleType;
    }
}