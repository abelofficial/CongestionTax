using CongestionTaxCalculator.Api.Models;

namespace CongestionTaxCalculator.Api.Services;

public class Calculator
{
    private readonly ITaxRule _taxRule;

    public Calculator(ITaxRule taxRule)
    {
        _taxRule = taxRule;
    }
    public int GetTax(IVehicle vehicle, DateTime[] dates)
    {
        DateTime intervalStart = dates[0];
        int totalFee = 0;
        foreach (DateTime date in dates)
        {
            int nextFee = _taxRule.GetTollFee(date, vehicle);
            int tempFee = _taxRule.GetTollFee(intervalStart, vehicle);
            var minutes = date.Subtract(intervalStart).TotalMinutes;

            if (minutes <= 60)
            {
                if (totalFee > 0) totalFee -= tempFee;
                if (nextFee >= tempFee) tempFee = nextFee;
                totalFee += tempFee;

            }
            else
            {
                totalFee += nextFee;
                intervalStart = date;
            }
        }
        if (totalFee > 60) totalFee = 60;

        return totalFee;
    }

}