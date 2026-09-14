# What framework/tool needs `Directory.Packages.props`?

## Question

What framework or tool needs `/Users/weiping/tmp/App/Directory.Packages.props`?

## Answer

**NuGet, via MSBuild's Central Package Management (CPM)** — a feature of the
.NET SDK (`Microsoft.NET.Sdk`) / NuGet, available since .NET 6 SDK (NuGet 6.2).

### How it's wired up in this repo

- `Directory.Build.props` sets `ManagePackageVersionsCentrally=true`. Once that
  is on, NuGet restore **requires** a `Directory.Packages.props` file walking up
  from each project — restore fails with `NU1507`/`NU1010` if it is missing or a
  package has no `PackageVersion` entry.
- `Directory.Packages.props` is auto-imported by the SDK (same convention as
  `Directory.Build.props`) and holds every
  `<PackageVersion Include="..." Version="..." />`.
- Projects then use `<PackageReference Include="System.CommandLine" />` with
  **no** `Version` attribute — e.g. `src/AsciiArt/AsciiArt.csproj`. Adding a
  `Version` there is an error (`NU1008`) under CPM.
- `CentralPackageTransitivePinningEnabled=true` in `Directory.Build.props` also
  makes those versions override transitive dependencies.

### Who reads it

- `dotnet restore` / `dotnet build`
- `dotnet package add <name>` (writes the new version into this file)
- Any IDE tooling that runs restore (C# Dev Kit, Rider, Visual Studio)

Nothing at runtime reads it — it is purely a restore-time input.

### Note

The file also sets `ManagePackageVersionsCentrally` itself, duplicating the
setting in `Directory.Build.props`. Harmless, but only one location is needed.
