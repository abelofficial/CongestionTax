namespace CongestionTaxCalculator.Api.Dtos;

public class CalculatorRequest
{
    public VehicleRequest? Vehicle { get; set; }
    public string[] Dates { get; set; } = new string[] { };
}
