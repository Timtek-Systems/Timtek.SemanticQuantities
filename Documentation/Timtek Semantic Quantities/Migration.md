# Migration

## From LegacyQuantity\<TUnit\> to Quantity (runtime)

Why migrate:
- Runtime Quantity allows late-bound units, parsing, and dynamic combinations more naturally.
- A single type simplifies APIs and reduces generic noise.

Typical changes:
- Construction:
  - Before: `new Quantity\<Meter\>(value)`
  - After:  `new Quantity(value, new Meter())` or use a domain factory (e.g., `Distance.FromMeters(value)`).
- Conversion:
  - Before: `legacy.As\<Foot\>()`
  - After:  `quantity.As(new Foot())` or `quantity.To(new Foot())` for a new quantity in that unit.
- Arithmetic:
  - Similar, but results now choose coherent units via the registry; in mixed systems, results default to SI.

Tips:
- Introduce domain factories (Distance/Duration/Angle/Speed/etc.) at call sites to make construction intention explicit.
- Use ToStringNormalized() when displaying results from mixed-unit operations.
- If you maintain your own registry (e.g., to register additional units), set DimensionSystem.Registry in your composition root.
