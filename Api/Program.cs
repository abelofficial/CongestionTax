using CongestionTaxCalculator.Api.Dtos;
using CongestionTaxCalculator.Api.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITaxRule, TaxRule>();

var app = builder.Build();
app.UseSwagger(x => x.SerializeAsV2 = true);
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.MapPost("/calculator", ([FromBody] CalculatorRequest req) =>
{
    try
    {
        var dates = req.Dates.Select(x => DateTime.Parse(x)).ToArray();
        return Results.Ok(new
        {
            result = new Calculator(new TaxRule()).GetTax(req.Vehicle, dates)
        });
    }
    catch
    {
        return Results.BadRequest(new { error = "Invalid date format." });
    }
});

app.Run();



