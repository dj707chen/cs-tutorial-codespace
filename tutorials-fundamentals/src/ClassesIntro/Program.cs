// https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/tutorials/system-command-line

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Text.Json;
/*
dotnet Program.cs

I'm using
    dotnet run --project src/ClassesIntro
to build and run ClassesIntro.csproj.
This loads its NuGet package references, including System.CommandLine.
*/

using Classes;

Console.WriteLine("Hello, ClassesIntro!");

var account = new BankAccount("<name>", 1000);
Console.WriteLine($"Account {account.Number} was created for {account.Owner} with {account.Balance} initial balance.");

account.MakeWithdrawal(500, DateTime.Now, "Rent payment");
Console.WriteLine(account.Balance);
account.MakeDeposit(100, DateTime.Now, "Friend paid me back");
Console.WriteLine(account.Balance);

// Test for a negative balance.
try {
    account.MakeWithdrawal(750, DateTime.Now, "Attempt to overdraw");
} catch (InvalidOperationException e) {
    Console.WriteLine("Exception caught trying to overdraw");
    Console.WriteLine(e.ToString());
}

// Test that the initial balances must be positive.
BankAccount invalidAccount;
try {
    invalidAccount = new BankAccount("invalid", -55);
} catch (ArgumentOutOfRangeException e) {
    Console.WriteLine("Exception caught creating account with negative balance");
    Console.WriteLine(e.ToString());
}

Console.WriteLine();
Console.WriteLine(account.GetAccountHistory());
