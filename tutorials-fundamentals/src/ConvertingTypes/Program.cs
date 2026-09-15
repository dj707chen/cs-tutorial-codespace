// https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/tutorials/safely-cast-using-pattern-matching-is-and-as-operators

/*
    dotnet run --project src/ConvertingTypes
*/
var g = new Giraffe();
var a = new Animal();
FeedMammals(g);
FeedMammals(a);
// Output:
// Giraffe is eating.
// Animal is not a Mammal

SuperNova sn = new SuperNova();
TestForMammals(g);
TestForMammals(sn);
// Output:
// I am a Giraffe animal.
// SuperNova is not a Mammal

static void FeedMammals(Animal a)
{
    if (a is Mammal m) // combines the test with an initialization assignment. The assignment occurs to m only when the test succeeds.
    {
        m.Eat();
    }
    else
    {
        // variable 'm' is not in scope here, and can't be used.
        Console.WriteLine($"{a.GetType().Name} is not a Mammal");
    }
}

static void TestForMammals(object o)
{
    // You also can use the as operator and test for null
    // before referencing the variable.
    var m = o as Mammal;
    if (m != null)
    {
        Console.WriteLine(m.ToString());
    }
    else
    {
        Console.WriteLine($"{o.GetType().Name} is not a Mammal");
    }
}

class Animal
{
    public void Eat() { Console.WriteLine($"{GetType().Name} is eating."); }

    public override string ToString()
    {
        return $"I am a {GetType().Name} animal.";
    }
}
class Mammal : Animal { }

#pragma warning disable CA1852 // Seal all classes that aren't designed for inheritance.
class Giraffe : Mammal { }

class SuperNova { }
#pragma warning restore CA1852
