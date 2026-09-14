# Q: What does App.slnx do? Which framework/tool uses it?

`App.slnx` is a **.NET solution file** in the newer XML-based `.slnx` format — the replacement for the old, verbose `.sln` format.

## What it does

It groups the projects in this folder into one solution so they can be built, tested, and opened together:

- `src/App.Cli`, `src/App.Core`, `src/AsciiArt` (under a `src` solution folder)
- `tests/App.Core.Tests` (under a `tests` solution folder)

The `<Folder>` elements are virtual solution folders (for organizing in an IDE), not disk directories.

```xml
<Solution>
  <Folder Name="/src/">
    <Project Path="src/App.Cli/App.Cli.csproj" />
    <Project Path="src/App.Core/App.Core.csproj" />
    <Project Path="src/AsciiArt/AsciiArt.csproj" />
  </Folder>
  <Folder Name="/tests/">
    <Project Path="tests/App.Core.Tests/App.Core.Tests.csproj" />
  </Folder>
</Solution>
```

## What uses it

- **.NET SDK / `dotnet` CLI** — `dotnet build`, `dotnet test`, `dotnet run --project ...`, `dotnet sln add ...` all work on it (supported since .NET 9 SDK; this repo pins .NET 10 in `global.json`).
- **MSBuild** — the underlying build engine that parses it.
- **IDEs:** Visual Studio 2022 17.13+, VS Code with the C# Dev Kit extension, and JetBrains Rider all open `.slnx` directly.

## `.slnx` vs `.sln`

`.slnx` drops the GUIDs and per-configuration boilerplate, making it human-readable and much friendlier to diffs and merges.
