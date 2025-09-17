# Quantities

## Quantity (runtime)
Namespace: Timtek.SemanticQuantities.Quantities

Quantity is a readonly struct that:
- Stores its numeric value in coherent SI (`ValueSI`)
- Carries a declared display unit (`Unit`)
- Exposes the dimensional signature via `Unit.Signature`
- Provides `As(IUnit)`, `To(IUnit)`, `Normalize()`, `ToStringNormalized()`
- Implements `+`, `-`, unary `-`, `*`, `/` with dimension-awareness

Construction examples:

```csharp
// Create distances via domain factories
var d1 = Timtek.SemanticQuantities.Quantities.Domain.Distance.FromMeters(100.0); // 100 m
var d2 = new Timtek.SemanticQuantities.Quantities.Quantity(328.084, new Timtek.SemanticQuantities.Units.Foot()); // ~100 m

// Add distances (coerces to a coherent unit; mixed systems default to SI)
var d = d1 + d2; // coherent length unit (e.g., m)

// Convert to specific units
var meters = d.As(new Timtek.SemanticQuantities.Units.Meter());
var feet   = d.As(new Timtek.SemanticQuantities.Units.Foot());

// Derived quantity via division: speed = distance / duration
var t  = Timtek.SemanticQuantities.Quantities.Domain.Duration.FromSeconds(9.58);
var v  = d1 / t; // coherent unit for L/T (m/s)
var vN = v.Normalize(); // uses registry’s coherent unit for this signature (e.g., "m/s")
```

Operators and dimensional consistency:
- Adding/subtracting requires the same signature; otherwise throws `InvalidOperationException`
- Multiplying/Dividing composes signatures (`+`/`-`) and returns a quantity in the registry’s coherent unit

Formatting:
- `ToString()` formats using the declared unit
- `ToStringNormalized()` formats using the coherent unit for the current unit’s system (mixed defaults to SI)

Registry access:
```csharp
// Replace the global registry in your composition root if desired
Timtek.SemanticQuantities.Quantities.DimensionSystem.Registry = Timtek.SemanticQuantities.Registry.RegistryBootstrapper.BuildDefault();
```

## LegacyQuantity\<TUnit\> (Obsolete)
Namespace: Timtek.SemanticQuantities.Quantities

A generic quantity retained for source compatibility during migration. Prefer the runtime Quantity.

```csharp
// Legacy example – consider migrating to runtime Quantity
var legacy = new Timtek.SemanticQuantities.Quantities.Quantity\<Timtek.SemanticQuantities.Units.Meter\>(5.0);
var asFeet = legacy.As\<Timtek.SemanticQuantities.Units.Foot\>();
```

## DimensionSystem
Namespace: Timtek.SemanticQuantities.Quantities

Provides a global IDimensionRegistry. By default this is set to the DefaultDimensionRegistry via RegistryBootstrapper.BuildDefault(). Consumers can swap the registry in their composition root.
