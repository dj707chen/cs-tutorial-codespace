namespace App.Core.Greetings;

/// <summary>Produces greetings. Replace with real domain services.</summary>
public interface IGreetingService
{
    /// <summary>Returns a greeting for <paramref name="name"/>.</summary>
    string Greet(string name);
}
