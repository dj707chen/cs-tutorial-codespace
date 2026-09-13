// WorkWithIntegers();
void WorkWithIntegers()
{
    int a = 18;
    int b = 6;
    int c = a + b;
    Console.WriteLine("\nWorkWithIntegers:");
    Console.WriteLine(c);


    // subtraction
    c = a - b;
    Console.WriteLine(c);

    // multiplication
    c = a * b;
    Console.WriteLine(c);

    // division
    c = a / b;
    Console.WriteLine(c);
}

// OrderPrecedence();
void OrderPrecedence()
{
    int a = 5;
    int b = 4;
    int c = 2;
    int d = a + b * c;
    Console.WriteLine("\nOrderPrecedence:");
    Console.WriteLine(d);

    d = (a + b) * c;
    Console.WriteLine(d);

    d = (a + b) - 6 * c + (12 * 4) / 3 + 12;
    Console.WriteLine(d);

    int e = 7;
    int f = 4;
    int g = 3;
    int h = (e + f) / g;
    Console.WriteLine(h);
}

IntOps();
void IntOps() {
    int a = 7;
    int b = 4;
    int c = 3;
    int d = (a + b) / c;
    int e = (a + b) % c;
    Console.WriteLine("\nIntOps:");
    Console.WriteLine($"quotient: {d}");
    Console.WriteLine($"remainder: {e}");

    int max = int.MaxValue;
    int min = int.MinValue;
    Console.WriteLine($"The range of integers is {min} to {max}");

    int what = max + 3;
    Console.WriteLine($"An example of overflow: {what}");
}

DoubleOps();
void DoubleOps() {
    double max = double.MaxValue;
    double min = double.MinValue;
    Console.WriteLine("\nDoubleOps:");
    Console.WriteLine($"The range of double is {min} to {max}");
}

DecimalOps();
void DecimalOps() {
    decimal min = decimal.MinValue;
    decimal max = decimal.MaxValue;
    Console.WriteLine("\nDecimalOps:");
    Console.WriteLine($"The range of the decimal type is {min} to {max}");
}
