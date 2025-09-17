# Registry

## IDimensionRegistry
Core capabilities:
- `ParseUnit(text)`: resolve unit by symbol (case-sensitive) or name (case-insensitive)
- `IsKnownUnit(text)`
- `GetCoherentUnit(signature, preferredSystem = SI)`: returns preferred coherent unit; falls back to SI; if none found, returns a canonical `CompoundUnit`
- `Register(unit, system)`
- `GetUnitSystem(unit)`: infers SI/Imperial
- `Convert(Quantity, targetUnit)`

## DefaultDimensionRegistry
- Maintains symbol/name lookup tables and coherent-unit maps per system.
- Registers a broad set of units via `RegistryBootstrapper.BuildDefault()`.
- Mixed-system operands: coherent system chosen as SI by default.

## UnitExpressionParser
- Parses expressions like `kg*m/s^2`, `m/s^2`, `N*m` with integer exponents.
- Splits numerator/denominator, resolves units, sums signatures, remembers systems. If mixed, defaults result to SI.
- Returns a `CompoundUnit` with a canonical symbol if needed.

## UnitSystem
- Enum: `SI`, `Imperial`

## Examples
Parse and use a unit expression:
```csharp
var reg = Timtek.SemanticQuantities.Quantities.DimensionSystem.Registry;
if (Timtek.SemanticQuantities.Registry.UnitExpressionParser.TryParse("kg*m/s^2", reg, out var unit) && unit != null)
{
    var force = new Timtek.SemanticQuantities.Quantities.Quantity(12.3, unit); // Newton-equivalent
    var inN   = reg.Convert(force, new Timtek.SemanticQuantities.Units.Newton());
}
```

Obtain a coherent unit for a derived signature:
```csharp
var sig = Timtek.SemanticQuantities.Dimensions.DimensionSignature.Length - Timtek.SemanticQuantities.Dimensions.DimensionSignature.Time;
var coherent = reg.GetCoherentUnit(sig); // usually "m/s" (MeterPerSecond)
```
