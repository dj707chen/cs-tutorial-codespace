# App

A .NET 10 solution laid out to hold a large C# codebase: shared build settings in
one place, NuGet versions in one place, and a `src/` + `tests/` split.

## Layout

```
App.slnx                     Solution (XML format — no GUIDs, merges cleanly)
global.json                  Pins the SDK version
Directory.Build.props        Build settings for EVERY project
Directory.Packages.props     Every NuGet version in the repo (CPM)
nuget.config                 Locked to nuget.org only
.editorconfig                Formatting + analyzer severities
.config/dotnet-tools.json    Pinned local CLI tools
artifacts/                   All build output (no per-project bin/obj)

src/
  Directory.Build.props      Settings for shipping code only
  App.Core/                  Domain library (+ AddAppCore() composition root)
  App.Cli/                   Console host wiring App.Core via DI
tests/
  Directory.Build.props      xunit + test SDK applied to every test project
  App.Core.Tests/
```

## Everyday commands

```bash
dotnet sln App.slnx add src/ClassesIntro/ClassesIntro.csproj
dotnet sln App.slnx add src/NullableIntroduction/NullableIntroduction.csproj 
dotnet sln App.slnx add src/TaskCli/TaskCli.csproj                          

dotnet restore                       # restore all projects
dotnet build                         # build the solution
dotnet test                          # run all tests
dotnet run --project src/App.Cli -- Ada
dotnet run --project src/AsciiArt -- Ada
dotnet format                        # apply .editorconfig formatting
dotnet tool restore                  # install pinned local tools
```

## Adding a project

```bash
dotnet new classlib -o src/App.Billing -n App.Billing
dotnet sln App.slnx add src/App.Billing/App.Billing.csproj
```

Then delete `TargetFramework`, `Nullable`, and `ImplicitUsings` from the new
`.csproj` — they are inherited from `Directory.Build.props`. A `.csproj` in this
repo should contain only what is specific to that project: `OutputType`,
`ProjectReference`, `PackageReference`.

Test projects need no package references at all; `tests/Directory.Build.props`
supplies xunit, the test SDK, and coverage to everything under `tests/`.

## Managing dependencies

Central Package Management is on, so **versions never appear in a `.csproj`**.

```bash
# From the project folder — the version is written to Directory.Packages.props
# and the versionless <PackageReference> to the .csproj.
cd src/App.Core && dotnet package add Serilog

# See what is out of date across the whole solution
dotnet dotnet-outdated

# What a project actually pulls in, transitively
dotnet list package --include-transitive
dotnet list package --vulnerable --include-transitive
```

To use a package from more than one project, add the `<PackageReference
Include="..." />` (no version) to each `.csproj`; they all share the single
`<PackageVersion>` entry. `CentralPackageTransitivePinningEnabled` is on, so a
`PackageVersion` entry also pins transitive dependencies — the fix for a
vulnerable indirect package is one line in `Directory.Packages.props`.

Dependabot (`.github/dependabot.yml`) opens weekly grouped PRs against
`Directory.Packages.props`.

## Code quality

`TreatWarningsAsErrors` and `EnforceCodeStyleInBuild` are on, with
`AnalysisLevel=latest-recommended`. This is deliberate: on a large codebase,
warnings that are not errors accumulate until nobody reads them. Tune severities
in `.editorconfig` (repo-wide) rather than with `#pragma` or per-project
`NoWarn`.

Public members under `src/` require XML doc comments (CS1591 is un-suppressed in
`src/Directory.Build.props`).

## Renaming

The `App` prefix is a placeholder. To rebrand, rename the project folders and
`.csproj` files, then find/replace `App.` and the `App` namespaces — do it now
while there are five files, not later.
