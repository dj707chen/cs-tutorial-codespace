using App.Core.Greetings;

using Microsoft.Extensions.Logging.Abstractions;

namespace App.Core.Tests.Greetings;

public sealed class GreetingServiceTests
{
    private readonly GreetingService _sut = new(NullLogger<GreetingService>.Instance);

    [Fact]
    public void Greet_returns_greeting_for_name()
    {
        Assert.Equal("Hello, Ada!", _sut.Greet("Ada"));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Greet_rejects_blank_names(string name)
    {
        Assert.Throws<ArgumentException>(() => _sut.Greet(name));
    }
}
