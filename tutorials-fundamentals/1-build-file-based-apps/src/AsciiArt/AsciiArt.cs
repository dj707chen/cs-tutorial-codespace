// 2. Add the necessary using statements at the top of your file
using System.CommandLine;
using System.CommandLine.Parsing;

/*
https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/tutorials/file-based-programs#unix-shebang--support
This shebang uses env to find dotnet in the PATH environment.
The -S parameter enables env to pass dotnet and -- as separate arguments. 
The -- ensures that any arguments a user provides are passed directly to your app, preventing dotnet from consuming them by mistake.

How to run the app:
dotnet AsciiArt.cs -- This is the command line.
AsciiArt.cs how are you ying\?
*/

Console.WriteLine("Hello, world!");

// Read command line arguments
// https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/tutorials/file-based-programs#read-command-line-arguments
if (args.Length > 0)
{
    string message = string.Join(' ', args);
    Console.WriteLine(message);
}

// Write ASCII art output
// https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/tutorials/file-based-programs#write-ascii-art-output
if (args.Length > 0)
{
    string message = string.Join(' ', args);
    Colorful.Console.WriteAscii(message);
}
/*
Run
    dotnet AsciiArt.cs -- This is the command line.
Output:
_  __/  / /   (_)  ___       (_)  ___      / /_  / /  ___       ____ ___   __ _   __ _  ___ _  ___  ___/ /       / /  (_)  ___  ___    
 / /    / _ \ / /  (_-<      / /  (_-<     / __/ / _ \/ -_)     / __// _ \ /  ' \ /  ' \/ _ `/ / _ \/ _  /       / /  / /  / _ \/ -_) _ 
/_/    /_//_//_/  /___/     /_/  /___/     \__/ /_//_/\__/      \__/ \___//_/_/_//_/_/_/\_,_/ /_//_/\_,_/       /_/  /_/  /_//_/\__/ (_)
*/

// Process command options
// https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/tutorials/file-based-programs#process-command-options

// 3. Define the delay option and messages argument
// Uncomment then Command-Click on the Option class to see the documentation for the Option class in the System.CommandLine namespace.
// Option dummyOption;
Option<int> delayOption = new("--delay") // Same as new Option<int>("--delay")
{
    Description = "Delay between lines, specified as milliseconds.",
    DefaultValueFactory = parseResult => 100
};

Argument<string[]> messagesArgument = new("Messages")
{
    Description = "Text to render."
};

// 4. Create a root command and configure it with the option and argument.
RootCommand rootCommand = new("Ascii Art file-based app sample");
rootCommand.Options.Add(delayOption);
rootCommand.Arguments.Add(messagesArgument);

// 5. Add the code to parse the command line arguments and handle any errors
ParseResult result = rootCommand.Parse(args);
foreach (ParseError parseError in result.Errors)
{
    Console.Error.WriteLine(parseError.Message);
}

////////////////
// Use parsed command line results
// https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/tutorials/file-based-programs#use-parsed-command-line-results
var parsedArgs = await ProcessParseResults(result);

await WriteAsciiArt(parsedArgs);
return 0;

async Task<AsciiMessageOptions> ProcessParseResults(ParseResult result)
{
    int delay = result.GetValue(delayOption);
    List<string> messages = [.. result.GetValue(messagesArgument) ?? Array.Empty<string>()];

    // If no messages are provided, read from standard input until an empty line is entered
    if (messages.Count == 0)
    {
        while (Console.ReadLine() is string line && line.Length > 0)
        {
            Colorful.Console.WriteAscii(line);
            await Task.Delay(delay);
        }
    }
    return new([.. messages], delay);
}

async Task WriteAsciiArt(AsciiMessageOptions options)
{
    foreach (string message in options.Messages)
    {
        Colorful.Console.WriteAscii(message);
        await Task.Delay(options.Delay);
    }
}

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CA1050 // Declare types in namespaces
public record AsciiMessageOptions(string[] Messages, int Delay);
#pragma warning restore CA1050 // Declare types in namespaces
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member