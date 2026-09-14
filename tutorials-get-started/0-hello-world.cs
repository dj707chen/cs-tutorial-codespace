// https://learn.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/tutorials/hello-world
Console.WriteLine("Hello, World! 09/13");

string aFriend = "Bill";
Console.WriteLine(aFriend);

aFriend = "Kellie";
Console.WriteLine(aFriend);

Console.WriteLine("Hello " + aFriend);
Console.WriteLine($"Hello {aFriend}");

string firstFriend = "Maria";
string secondFriend = "Sage";
Console.WriteLine($"My friends are {firstFriend} and {secondFriend}");

Console.WriteLine($"The name {firstFriend} has {firstFriend.Length} letters.");
Console.WriteLine($"The name {secondFriend} has {secondFriend.Length} letters.");

// Remove whitespace from strings
// https://learn.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/tutorials/hello-world#remove-whitespace-from-strings
string greeting = "      Hello World!       ";
Console.WriteLine();
Console.WriteLine($"[{greeting}]");

string trimmedGreeting = greeting.TrimStart();
Console.WriteLine($"[{trimmedGreeting}]");

trimmedGreeting = greeting.TrimEnd();
Console.WriteLine($"[{trimmedGreeting}]");

trimmedGreeting = greeting.Trim();
Console.WriteLine($"[{trimmedGreeting}]");

string sayHello = "Hello World!";
Console.WriteLine();
Console.WriteLine(sayHello);
sayHello = sayHello.Replace("Hello", "Greetings");
Console.WriteLine(sayHello);

Console.WriteLine(sayHello.ToUpper());
Console.WriteLine(sayHello.ToLower());
