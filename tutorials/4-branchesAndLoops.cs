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
while (counter < 10)
{
    Console.WriteLine($"Hello World! The counter is {counter}");
    counter++;
}
