using App.Core;
using App.Core.Greetings;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddAppCore();

using var host = builder.Build();

var greeter = host.Services.GetRequiredService<IGreetingService>();
Console.WriteLine(greeter.Greet(args.Length > 0 ? args[0] : "world"));
