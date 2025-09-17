# WARP.md

This file provides guidance to WARP (warp.dev) when working with code in this repository.

Project overview
- Solution: Timtek.SemanticQuantities.sln
- Projects:
  - SemanticQuantities/Timtek.SemanticQuantities.csproj (Library) – targets net8.0 and netstandard2.0
  - SemanticQuantities.Tests/Timtek.SemanticQuantities.Tests.csproj (MSpec tests) – targets net8.0 and net48
- Central package management: Directory.Packages.props (pins Machine.Specifications, Machine.Fakes.FakeItEasy, MSpec console runner versions)
- Versioning: GitVersion.yaml (GitFlow-style configuration; tags: main/master untagged, develop: -alpha, release: -rc, feature: branch name, hotfix/support: untagged)

Common commands (pwsh on Windows)
- Restore
  - dotnet restore .
- Build
  - dotnet build Timtek.SemanticQuantities.sln -c Debug
  - Cross-target build (library only): dotnet build .\SemanticQuantities\Timtek.SemanticQuantities.csproj -c Release -f net8.0
- Format/lint (no repo-specific analyzers configured; use dotnet-format)
  - dotnet format --verify-no-changes
  - dotnet format style --severity info
  - dotnet format analyzers --severity info
- Run tests (MSpec console runner against net48 output)
  1) Build tests for .NET Framework (required by MSpec console runner):
     - dotnet build .\SemanticQuantities.Tests\Timtek.SemanticQuantities.Tests.csproj -c Debug -f net48
  2) Locate the MSpec console runner from the NuGet cache and execute:
     - $mspec = Get-ChildItem "$env:USERPROFILE\.nuget\packages\machine.specifications.runner.console\*\tools\**\mspec*.exe" -File | Sort-Object LastWriteTime -Descending | Select-Object -First 1
     - & $mspec .\SemanticQuantities.Tests\bin\Debug\net48\Timtek.SemanticQuantities.Tests.dll --progress
- Run a single test/spec (via tag include)
  - Add [Tags("focus")] to the specific spec you want to run
  - & $mspec .\SemanticQuantities.Tests\bin\Debug\net48\Timtek.SemanticQuantities.Tests.dll --include focus --progress
  - Remove the tag afterwards to avoid accidental filtering

High-level architecture
- Quantities core
  - IUnit (SemanticQuantities/Units/IUnit.cs) defines unit conversion to/from SI.
  - Quantity<TUnit> (SemanticQuantities/Quantities/Quantity.cs) is a readonly struct that stores its value in SI units and converts on demand using the unit type parameter. This ensures consistent internal representation and strong typing at compile time.
  - Units (SemanticQuantities/Units/*): concrete unit classes implement IUnit (e.g., Second, JulianDay; additional units like Degree/Radian are used by domain helpers). Each unit contains only ToSI/FromSI conversions.
  - Domain helpers (SemanticQuantities/Quantities/Domain/*): convenience factories for common domain concepts using Quantity<TUnit>, e.g., Angle.FromDegrees/FromRadians and Direction.FromXYZ (normalized unit vector).
- Time system
  - ITimeScale and ITimeScaleContext define the abstraction for working across time scales.
  - TimeInstant<TScale> represents an instant anchored internally in TAI seconds; conversion to/from other scales is delegated to ITimeScale and requires an ITimeScaleContext.
  - Concrete scales: TAI (identity), TT (fixed +32.184 s offset), UTC (depends on leap seconds; solved via a simple fixed-point iteration for FromTaiSeconds).
  - Context and providers: BasicTimeScaleContext wires providers for leap seconds, EOP, and ephemerides. BasicLeapSecondsProvider currently returns 0 for all dates (stub), so UTC/TAI conversions are identical until a real provider is supplied. IEopProvider and IEphemerisProvider are defined for future accuracy-sensitive features.

Notes for working in this repo
- The test project targets net48 in addition to net8.0 to support the MSpec console runner; prefer running tests via the console runner against the net48 build output.
- Nullability is disabled in the tests project (as is common with MSpec), but enabled in the library.
- Package versions are centrally managed; if you add or update packages, prefer modifying Directory.Packages.props.
