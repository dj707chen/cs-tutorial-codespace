// https://learn.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/tutorials/tuples-and-types

var pt = (X: 1, Y: 2);

// var is only used by itself, like var slope = ...; not var double slope.
// var double slope = (double)pt.Y / (double)pt.X; // This is not valid syntax.
// The following two lines are valid syntax.
// double slope = (double)pt.Y / (double)pt.X;
var slope = (double)pt.Y / (double)pt.X;
Console.WriteLine($"A line from the origin to the point {pt} has a slope of {slope}.");

pt.X = pt.X + 5;
Console.WriteLine($"The point is now at {pt}.");

var pt2 = pt with { Y = 10 };
Console.WriteLine($"The point 'pt2' is at {pt2}.");

var subscript = (A: 0, B: 0);
subscript = pt;
Console.WriteLine(subscript);

var namedData = (Name: "Morning observation", Temp: 17, Wind: 4);
var person = (FirstName: "", LastName: "");
var order = (Product: "guitar picks", style: "triangle", quantity: 500, UnitPrice: 0.10m);
