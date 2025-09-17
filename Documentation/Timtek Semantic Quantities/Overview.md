# Overview

SemanticQuantities is a .NET library that represents physical quantities with explicit units and dimensional signatures. Values are stored in coherent SI internally, and conversions are performed through a registry of units. Angle is modeled as a distinct base dimension (separate from dimensionless), which improves safety and clarity when working with rotations and astronomical angles.

Key ideas:
- Coherent SI storage: runtime Quantity stores numeric values as SI, regardless of construction unit.
- Units are first-class: each unit implements IUnit with ToSI/FromSI and a DimensionSignature.
- Dimension-aware operators: arithmetic checks and composes dimensional signatures.
- Registry-driven behavior: DefaultDimensionRegistry resolves symbols/names, coherent units, and unit systems (SI/Imperial). Mixed-system operations default to SI.
- Angle as a base dimension: avoids accidental mixing of angles with pure scalars and improves clarity in astronomical contexts.

Targets:
- Library: net8.0 and netstandard2.0
- Tests: net8.0 and net48 (for MSpec console runner compatibility)

Folder highlights (library):
- SemanticQuantities/Dimensions – DimensionSignature
- SemanticQuantities/Units – IUnit and concrete units (SI base, derived, accepted non-SI, Imperial)
- SemanticQuantities/Quantities – Quantity (runtime), LegacyQuantity<TUnit> (obsolete), DimensionSystem
- SemanticQuantities/Registry – DefaultDimensionRegistry, IDimensionRegistry, UnitExpressionParser, UnitSystem, RegistryBootstrapper
- SemanticQuantities/Time – TimeInstant<TScale>, time scales (UTC, TAI, TT), context/providers
- SemanticQuantities/Quantities/Domain – convenience factories and domain types

See also:
- [[Architecture]] for a diagram and component relationships
- [[Rationale]] for design motivations and trade-offs
