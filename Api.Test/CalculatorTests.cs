using CongestionTaxCalculator.Api.Models;
using CongestionTaxCalculator.Api.Services;
using FluentAssertions;

namespace Api.Test;

public class UnitTest1
{
    [Theory]
    [InlineData("2013-02-07 06:05:00", 8)]
    [InlineData("2013-02-07 06:35:00", 13)]
    [InlineData("2013-02-07 07:30:00", 18)]
    [InlineData("2013-02-07 08:05:00", 13)]
    [InlineData("2013-02-07 08:35:00", 8)]
    [InlineData("2013-02-07 15:05:00", 13)]
    [InlineData("2013-02-07 15:35:00", 18)]
    [InlineData("2013-02-07 17:05:00", 13)]
    [InlineData("2013-02-07 18:05:00", 8)]
    [InlineData("2013-02-07 18:35:00", 0)]
    public void should_calculate_the_proper_tax_fee_for_all_frames(string dateString, int expected)
    {
        var date = DateTime.Parse(dateString);
        var result = new Calculator(new TaxRule()).GetTax(new Car(), new DateTime[] { date });

        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("2013-02-07 06:35:00", "2013-02-07 06:35:00", 13)]
    public void should_take_highest_when_time_is_within_60mins(string dateOne, string dateTwo, int expected)
    {
        var result = new Calculator(new TaxRule()).GetTax(new Car(), new DateTime[] {
            DateTime.Parse(dateOne),
            DateTime.Parse(dateTwo)
         });

        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("2013-02-07 08:35:00", "2013-02-07 18:05:00", 16)]
    [InlineData("2013-02-07 06:35:00", "2013-02-07 17:05:00", 26)]
    [InlineData("2013-02-07 08:25:00", "2013-02-07 15:35:00", 31)]
    public void should_take_sum_when_time_is_not_within_60mins(string lowestCostDate, string highestCostDate, int expected)
    {
        var result = new Calculator(new TaxRule()).GetTax(new Car(), new DateTime[] {
            DateTime.Parse(lowestCostDate),
            DateTime.Parse(highestCostDate)
         });

        result.Should().Be(expected);
    }

    [Fact]
    public void should_not_charge_more_than_60_tax_fee_per_day()
    {
        var result = new Calculator(new TaxRule()).GetTax(new Car(), new DateTime[] {
            DateTime.Parse("2013-02-07 06:35:00"), // 8
            DateTime.Parse("2013-02-07 07:40:00"), // 18
            DateTime.Parse("2013-02-07 08:55:00"), // 13
            DateTime.Parse("2013-02-07 13:35:00"), // 8
            DateTime.Parse("2013-02-07 15:20:00"), // 18
            DateTime.Parse("2013-02-07 16:35:00"), // 18
            DateTime.Parse("2013-02-07 17:35:00") // 8
         });

        result.Should().Be(60);
    }
}