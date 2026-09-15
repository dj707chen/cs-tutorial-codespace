using Microsoft.Extensions.Logging;

namespace App.Core.Greetings;

/// <inheritdoc cref="IGreetingService" />
public sealed partial class GreetingService(ILogger<GreetingService> logger) : IGreetingService {
    private readonly ILogger<GreetingService> _logger = logger;

    /// <inheritdoc />
    public string Greet(string name) {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        LogGreeting(name);
        return $"Hello, {name}!";
    }

    // Source-generated logging: allocation-free and satisfies CA1848, https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/quality-rules/ca1848
    [LoggerMessage(Level = LogLevel.Information, Message = "Greeting {Name}")]
    private partial void LogGreeting(string name);
}

/*
Explain the definition of GreetingService class:
    public sealed partial class GreetingService(ILogger<GreetingService> logger) : IGreetingService
public — visible to other assemblies (e.g. App.Cli and the test project).
sealed — no class can inherit from it. This is a good default for classes not designed as base classes:
        it signals intent, and the JIT can devirtualize calls on sealed types.
partial — the class body may be split across multiple files;
        The other "half" here isn't written by you: the [LoggerMessage] attribute on line 20 triggers
        a source generator that emits the body of LogGreeting at compile time into a generated file;
        partial on the class (and on the method) is what allows the generator to add code to it.
(ILogger<GreetingService> logger) — a primary constructor (C# 12). It declares the constructor parameters directly in the class header,
        so you don't write an explicit constructor. logger is then in scope throughout the class body,
        and line 8 copies it into a readonly field: _logger
        Copying to a field is optional (you could use logger directly in methods) but it makes the captured state explicit and readonly.
: IGreetingService — implements the interface in IGreetingService.cs;
        Callers depend on the interface, so the implementation can be swapped or mocked in tests.
/// <inheritdoc cref="IGreetingService" /> — pulls the XML doc comment from the interface instead of duplicating it.
        The build has GenerateDocumentationFile on, so this keeps the docs in one place.
*/
