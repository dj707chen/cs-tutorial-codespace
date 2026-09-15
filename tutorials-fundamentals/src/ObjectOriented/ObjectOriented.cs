// https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/tutorials/system-command-line

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Text.Json;
/*
dotnet ClassesIntro.cs -- add "Write documentation" --priority High --due 2026-04-01
dotnet ClassesIntro.cs -- list --all
dotnet ClassesIntro.cs -- complete 3
dotnet ClassesIntro.cs -- remove 3
dotnet ClassesIntro.cs -- --verbose list

I'm using
    dotnet run --project src/ClassesIntro
to build and run ClassesIntro.csproj.
This loads its NuGet package references, including System.CommandLine.
*/

using ObjectOriented;

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

Console.WriteLine();
Console.WriteLine("----------------------------------------------------------------------------------------------------------");
Console.WriteLine();
Console.WriteLine("--- GiftCardAccount ---");
var giftCard = new GiftCardAccount("gift card", 100, 50);
giftCard.MakeWithdrawal(20, DateTime.Now, "get expensive coffee");
giftCard.MakeWithdrawal(50, DateTime.Now, "buy groceries");
giftCard.PerformMonthEndTransactions();
// can make additional deposits:
giftCard.MakeDeposit(27.50m, DateTime.Now, "add some additional spending money");
Console.WriteLine(giftCard.GetAccountHistory());

Console.WriteLine("--- InterestEarningAccount ---");
var savings = new InterestEarningAccount("savings account", 10000);
savings.MakeDeposit(750, DateTime.Now, "save some money");
savings.MakeDeposit(1250, DateTime.Now, "Add more savings");
savings.MakeWithdrawal(250, DateTime.Now, "Needed to pay monthly bills");
savings.PerformMonthEndTransactions();
Console.WriteLine(savings.GetAccountHistory());

Console.WriteLine("--- LineOfCreditAccount ---");
var lineOfCredit = new LineOfCreditAccount("line of credit", 0, 2000);
// How much is too much to borrow?
lineOfCredit.MakeWithdrawal(1000m, DateTime.Now, "Take out monthly advance");
lineOfCredit.MakeDeposit(50m, DateTime.Now, "Pay back small amount");
lineOfCredit.MakeWithdrawal(5000m, DateTime.Now, "Emergency funds for repairs");
lineOfCredit.MakeDeposit(150m, DateTime.Now, "Partial restoration on repairs");
lineOfCredit.PerformMonthEndTransactions();
Console.WriteLine(lineOfCredit.GetAccountHistory());
