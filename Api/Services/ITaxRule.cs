using CongestionTaxCalculator.Api.Models;

namespace CongestionTaxCalculator.Api.Services;

public interface ITaxRule
{
    bool IsTollFreeVehicle(IVehicle vehicle);

    int GetTollFee(DateTime date, IVehicle vehicle);

    Boolean IsTollFreeDate(DateTime date);
}