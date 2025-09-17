# Examples

## Mixed systems normalize to SI
```csharp
var feet = new Timtek.SemanticQuantities.Quantities.Quantity(3.0, new Timtek.SemanticQuantities.Units.Foot());
var m    = new Timtek.SemanticQuantities.Quantities.Quantity(1.0, new Timtek.SemanticQuantities.Units.Meter());
var sum  = feet + m; // coherent SI unit (e.g., "m")
```

## Derived unit from arithmetic
```csharp
var d = Timtek.SemanticQuantities.Quantities.Domain.Distance.FromMeters(100);
var t = Timtek.SemanticQuantities.Quantities.Domain.Duration.FromSeconds(12.5);
var v = d / t; // m/s
Console.WriteLine(v.ToStringNormalized());
```

## Unit-expression parsing
```csharp
var reg = Timtek.SemanticQuantities.Quantities.DimensionSystem.Registry;
if (Timtek.SemanticQuantities.Registry.UnitExpressionParser.TryParse("kg*m/s^2", reg, out var unit) && unit != null)
{
    var f = new Timtek.SemanticQuantities.Quantities.Quantity(42, unit);
    Console.WriteLine(f.ToString());
}
```

## Time conversion (UTC → TAI → TT)
```csharp
var ctx = new Timtek.SemanticQuantities.Time.BasicTimeScaleContext(new Timtek.SemanticQuantities.Time.NistLeapSecondsProvider());
var utc = Timtek.SemanticQuantities.Time.TimeInstant<Timtek.SemanticQuantities.Time.UTC>.FromSeconds(1_500_000_000, ctx);
var tai = utc.AsSeconds<Timtek.SemanticQuantities.Time.TAI>(ctx);
var tt  = utc.AsSeconds<Timtek.SemanticQuantities.Time.TT>(ctx);
```
