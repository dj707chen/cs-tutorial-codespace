using System.CommandLine;

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