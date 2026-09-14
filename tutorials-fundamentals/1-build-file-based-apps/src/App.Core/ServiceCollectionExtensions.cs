using App.Core.Greetings;

using Microsoft.Extensions.DependencyInjection;

namespace App.Core;

/// <summary>
/// Composition root for this library. Each library owns one of these so hosts
/// wire up a whole assembly with a single call instead of registering types
/// one by one.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>Registers the services provided by <c>App.Core</c>.</summary>
    public static IServiceCollection AddAppCore(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddSingleton<IGreetingService, GreetingService>();
        return services;
    }
}
