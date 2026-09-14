// Branches and loops
// https://learn.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/tutorials/branches-and-loops

int a = 5;
int b = 6;
int c = 3;

if (a + b > 10)
    Console.WriteLine("The answer a+b is greater than 10.");
else
    Console.WriteLine("The answer a+b is not greater than 10");
if (a + c > 10)
    Console.WriteLine("The answer a+c is greater than 10.");
else
    Console.WriteLine("The answer a+c is not greater than 10");

// Use loops to repeat operations
// https://learn.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/tutorials/branches-and-loops#use-loops-to-repeat-operations
int counter = 0;
Console.WriteLine();
while (counter < 6)
{
    Console.WriteLine($"Hello World! The counter is {counter}");
    counter++;
}

// https://learn.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/tutorials/branches-and-loops#created-nested-loops
Console.WriteLine();
for (int row = 1; row < 5; row++)
{
    for (char column = 'a'; column < 'k'; column++)
    {
        Console.WriteLine($"The cell is ({row}, {column})");
    }
}

// https://learn.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/tutorials/branches-and-loops#combine-branches-and-loops
int sum = 0;
Console.WriteLine();
for (int number = 1; number < 21; number++)
{
    if (number % 3 == 0)
    {
        sum = sum + number;
    }
}
Console.WriteLine($"The sum is {sum}");
