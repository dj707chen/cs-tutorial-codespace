// Create deposits and withdrawals
//   https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/tutorials/classes#create-deposits-and-withdrawals

namespace ObjectOriented;

public record Transaction(decimal Amount, DateTime Date, string Notes);

