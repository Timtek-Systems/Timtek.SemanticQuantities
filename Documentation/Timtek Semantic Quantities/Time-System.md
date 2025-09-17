# Time System

## TimeInstant\<TScale\>
- Represents an instant anchored in TAI seconds internally
- `FromSeconds(secondsSinceEpochInThisScale, context)`
- `AsSeconds<TOther>(context)`
- `Add(Quantity<Second>)`, `Subtract(TimeInstant<TScale>)`

## Scales
- `TAI`: identity to/from TAI seconds
- `TT`: fixed +32.184 s offset relative to TAI
- `UTC`: depends on leap seconds (`TAI - UTC` = table-driven, changes at leap instants)

## Context and providers
- `ITimeScaleContext` supplies providers (`ILeapSecondsProvider`, `IEopProvider?`, `IEphemerisProvider?`)
- `BasicTimeScaleContext` wires a `BasicLeapSecondsProvider` by default (returns 0 for all dates)
- `NistLeapSecondsProvider` implements a real UTC leap-second table (1972 onward) with optional extra steps

## Examples
```csharp
var ctx = new Timtek.SemanticQuantities.Time.BasicTimeScaleContext(
    leapSeconds: new Timtek.SemanticQuantities.Time.NistLeapSecondsProvider());

// Create a UTC instant from seconds since Unix epoch in UTC-scale
var utcInstant = Timtek.SemanticQuantities.Time.TimeInstant\<Timtek.SemanticQuantities.Time.UTC\>.FromSeconds(1_600_000_000, ctx);

// Convert to TAI seconds since epoch
var taiSeconds = utcInstant.AsSeconds\<Timtek.SemanticQuantities.Time.TAI\>(ctx);

// Convert to TT
var ttSeconds = utcInstant.AsSeconds\<Timtek.SemanticQuantities.Time.TT\>(ctx);
```

Notes:
- With `BasicLeapSecondsProvider` (stub), UTC and TAI produce identical numbers; use `NistLeapSecondsProvider` for correct offsets from 1972 onward.
