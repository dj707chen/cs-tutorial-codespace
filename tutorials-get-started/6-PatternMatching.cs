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
var reader = new StringReader(bankRecords);

// ? after type names. This symbol allows the value of this type to be null. For example, string? means that the variable can hold either a string value or null.
string? line;
while ((line = reader.ReadLine()) is not null)
{
    if (string.IsNullOrWhiteSpace(line)) continue;
    // Split the line based on comma delimiter and trim each part
    string[] parts = line.Split(',');

    // Use ? after parts[0] to handle potential null values, this is important because if parts[0] is null, calling Trim() on it would throw a NullReferenceException.
    // By using the null-conditional operator (?.), we ensure that Trim() is only called if parts[0] is not null, preventing potential runtime errors.
    // then transactionType must be declared as string? to allow for null values.
    string? transactionType = parts[0]?.Trim();
    if (double.TryParse(parts[1].Trim(), out double amount))
    {
        // Update the balance based on transaction type
        if (transactionType?.ToUpper() is "DEPOSIT")
            currentBalance += amount;
        else if (transactionType?.ToUpper() is "WITHDRAWAL")
            currentBalance -= amount;

        Console.WriteLine($"{line.Trim()} => Parsed Amount: {amount}, New Balance: {currentBalance}");
    } else
    {
        Console.WriteLine($"{line.Trim()} => Failed to parse amount from line");
    }
}

//////////////////////////////
//  Enum matches
// https://learn.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/tutorials/pattern-matching#enum-matches

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
    if (transaction.type == TransactionType.Deposit)
        currentBalance += transaction.amount;
    else if (transaction.type == TransactionType.Withdrawal)
        currentBalance -= transaction.amount;
    Console.WriteLine($"{transaction.type} \t=> Parsed Amount: {transaction.amount}, \tNew Balance: {currentBalance}");
}

public enum TransactionType
{
    Deposit,
    Withdrawal,
    Invalid
}
