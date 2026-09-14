// Create record types
// https://learn.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/tutorials/tuples-and-types#create-record-types
Point pt3 = new Point(1, 1);
var pt4 = pt3 with { Y = 10 };
Console.WriteLine($"The two points are {pt3} and {pt4}");

double slopeResult = pt4.Slope();
Console.WriteLine($"The slope of {pt4} is {slopeResult}");

public record struct Point(int X, int Y) {
// public record     Point(int X, int Y) { // default is class, but you can also use struct
    public double Slope() => (double)Y / (double)X;
}
/*
Place the preceding code at the bottom of your source file.
Type declarations like record declarations must follow executable statements in a file-based app.
*/
