# Congestion Tax Calculator

## Solution

In order to make the Congestion Tax Calculator service accessible through Http, I am using minimal API and exposed the calculator through the `/calculator` endpoint. The endpoint takes the following parameter.

```json
{
    {
        "vehicle": {
            "vehicleType": "Car"
        },
        "dates": [ "2013-01-14 21:00:00", "2013-02-07 06:23:27" ]
    }
}
```

and then returns the calculated value as follows.

```json
{
  "result": 8
}
```

## Bugs found

- Total tax fee was returning incorrect value for date values with 60 minute plus difference.
  - Solution: was to update the intervalStart variables when the time difference with the previous datetime is greater than 60;
- The type for Motorcycle vehicle was spelled differently on TollFreeVehicles enum from the one in the class definition.
  - Solution: Updated the type for Motorcycle vehicle to "Motorcycle".

## Running the API

```bash
dotnet run --project Api
```

## Running the Tests

```bash
dotnet test --project Api.Test
```
