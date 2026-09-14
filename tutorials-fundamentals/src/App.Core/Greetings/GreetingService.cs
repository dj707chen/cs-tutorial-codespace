using Microsoft.Extensions.Logging;

namespace App.Core.Greetings;

/// <inheritdoc cref="IGreetingService" />
public sealed partial class GreetingService(ILogger<GreetingService> logger) : IGreetingService
{
    private readonly ILogger<GreetingService> _logger = logger;

    /// <inheritdoc />
    public string Greet(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        LogGreeting(name);
        return $"Hello, {name}!";
    }

    // Source-generated logging: allocation-free and satisfies CA1848.
    [LoggerMessage(Level = LogLevel.Information, Message = "Greeting {Name}")]
    private partial void LogGreeting(string name);
}
