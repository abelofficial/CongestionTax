namespace CongestionTaxCalculator.Api.Models;

public class Motorbike : IVehicle
{
    public string VehicleType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public Motorbike()
    {
        VehicleType = "Motorcycle";
    }

    public string GetVehicleType()
    {
        return VehicleType;
    }
}