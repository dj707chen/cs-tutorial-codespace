# Cmd+click on `Option` in a decompiled file does nothing, but `string` works

## Question

Inside `~/.dotnet/symbolcache/.../Option{T}.cs`, Cmd+clicking `string` on line 19
(`public Option(string name, params string[] aliases)`) opened the definition of
`System.String`, but Cmd+clicking `Option` at the end of line 10
(`public class Option<T> : Option`) did nothing.

## Answer

This is expected behavior given how that file was produced, not a bug in the project.

### Solution

> **Simplest: type `Option` somewhere in a project file, F12 it, then delete the text.**

Navigate to framework/NuGet types from your *own* source files, not from within
files in the symbol cache. Any type name typed into a project file (even
temporarily) has full project context, so Go to Definition resolves it and
fetches the decompiled/Source Link source. Once you land in the target file,
remove the temporary text.

### Why it happens

Files under `~/.dotnet/symbolcache/` are Source Link / decompiled sources that the
C# extension fetched on demand when you navigated *into*
`System.CommandLine.Option<T>` from your own code. They aren't part of any project
or workspace, so Roslyn has no real compilation for them. Navigation inside them
works only for symbols the language server can resolve without a project:

- `string` -> `System.String` in the BCL, which the server always knows about
  (it resolves against the reference assemblies of your target framework).
  Cmd+click works.
- `Option` -> the non-generic base class `System.CommandLine.Option`, defined in
  the *same* NuGet package. Reaching it would require fetching/decompiling a
  second file from that assembly, starting from a file with no project context.
  The Roslyn LSP doesn't chain that, so the click silently does nothing.

The same asymmetry applies to `Argument<T>` on line 12 and the
`System.CommandLine.Parsing` types: anything defined in `System.CommandLine.dll`
won't resolve from inside the cached file, while anything in the BCL will.

### Other options

- Prefer Go to Definition from the usage site in your own code rather than from
  within decompiled files.
- `"dotnet.navigation.navigateToDecompiledSources": true` and
  `"dotnet.navigation.navigateToSourceLinkAndEmbeddedSources": true` enable the
  initial jump, but not nested navigation from cached files.
- For fully navigable framework source, open the `System.CommandLine` repo
  directly.

This is a known limitation tracked in
[dotnet/vscode-csharp](https://github.com/dotnet/vscode-csharp)
("Go to Definition doesn't work from within decompiled/Source Link files").
