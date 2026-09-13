// https://learn.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/tutorials/pattern-matching

//////////////////////////////
//  Match a value

string bankRecords = """
    DEPOSIT,   10000, Initial balance
    DEPOSIT,     500, regular deposit
    WITHDRAWAL, 1000, rent
    DEPOSIT,    2000, freelance payment
    WITHDRAWAL,  300, groceries
    DEPOSIT,     700, gift from friend
    WITHDRAWAL,  150, utility bill
    DEPOSIT,    1200, tax refund
    WITHDRAWAL,  500, car maintenance
    DEPOSIT,     400, cashback reward
    WITHDRAWAL,  250, dining out
    DEPOSIT,    3000, bonus payment
    WITHDRAWAL,  800, loan repayment
    DEPOSIT,     600, stock dividends
    WITHDRAWAL,  100, subscription fee
    DEPOSIT,    1500, side hustle income
    WITHDRAWAL,  200, fuel expenses
    DEPOSIT,     900, refund from store
    WITHDRAWAL,  350, shopping
    DEPOSIT,    2500, project milestone payment
    WITHDRAWAL, 4w00, entertainment // Intentional malformed amount for testing
    """;

double currentBalance = 0.0;

///////////////////////////////////
// Exhaustive matches with switch
// https://learn.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/tutorials/pattern-matching#exhaustive-matches-with-switch
static IEnumerable<(TransactionType type, double amount)> TransactionRecords(string inputText)
{
    var reader = new StringReader(inputText);
    string? line;
    while ((line = reader.ReadLine()) is not null)
    {
        string[] parts = line.Split(',');

        string? transactionType = parts[0]?.Trim();
        if (double.TryParse(parts[1].Trim(), out double amount))
        {
            // Update the balance based on transaction type
            if (transactionType?.ToUpper() is "DEPOSIT")
                yield return (TransactionType.Deposit, amount);
            else if (transactionType?.ToUpper() is "WITHDRAWAL")
                yield return (TransactionType.Withdrawal, amount);
        }
        else {
            yield return (TransactionType.Invalid, 0.0);
        }
    }
}

currentBalance = 0.0;
Console.WriteLine();
Console.WriteLine("--------------------------------");
var transactions = TransactionRecords(bankRecords);
foreach (var transaction in transactions)
{
    currentBalance += transaction switch
    {
        (TransactionType.Deposit, var amount) => amount,
        (TransactionType.Withdrawal, var amount) => -amount,
        _ => 0.0,
    };
    Console.WriteLine($"{transaction.type} \t=> Parsed Amount: {transaction.amount}, \tNew Balance: {currentBalance}");
}

///////////////////////////////////
// Type patterns
// https://learn.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/tutorials/pattern-matching#type-patterns 

Console.WriteLine();
Console.WriteLine("--------------------------------");

public enum TransactionType
{
    Deposit,
    Withdrawal,
    Invalid
}
