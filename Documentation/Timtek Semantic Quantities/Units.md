# Units

## IUnit
Namespace: `Timtek.SemanticQuantities.Units`

`IUnit` defines:
- Name (e.g., "Meter")
- Symbol (e.g., "m")
- Signature (DimensionSignature)
- ToSI(value) and FromSI(siValue)

Units are immutable and encode only conversion rules and metadata.

## CompoundUnit
Represents canonical coherent compound units (e.g., "kg*m/s^2"). Conversion to SI is identity.

## SI base units (selection)
- Meter (m), Kilogram (kg), Second (s), Ampere (A), Kelvin (K), Mole (mol), Candela (cd), Radian (rad)

## Common derived units (selection)
- Frequency: Hertz (Hz)
- Force: Newton (N)
- Pressure: Pascal (Pa)
- Energy: Joule (J)
- Power: Watt (W)
- Charge: Coulomb (C)
- Potential: Volt (V)
- Resistance: Ohm (Ω)
- Conductance: Siemens (S)
- Capacitance: Farad (F)
- Magnetic flux/density: Weber (Wb), Tesla (T)
- Inductance: Henry (H)
- Luminous: Lumen (lm), Lux (lx)

## Accepted/non-SI convenience
- Kilometer (km), Degree (°), HourOfAngle (h_angle), Minute (min), Hour (h), Day (d), JulianDay (d_julian)

## Imperial/customary
- Foot (ft), Inch (in), Pound (lb), Ounce (oz), PoundForce (lbf), Psi (psi)

## Examples
Create and convert between units:
```csharp
var q1 = new Timtek.SemanticQuantities.Quantities.Quantity(10.0, new Timtek.SemanticQuantities.Units.Foot());
var q2 = q1.To(new Timtek.SemanticQuantities.Units.Meter());   // convert to meters
var s  = q2.ToString(); // e.g., "3.048 m"
```

Compose compound units via arithmetic:
```csharp
var d = new Timtek.SemanticQuantities.Quantities.Quantity(100.0, new Timtek.SemanticQuantities.Units.Meter());
var t = new Timtek.SemanticQuantities.Quantities.Quantity(9.58,  new Timtek.SemanticQuantities.Units.Second());
var v = d / t; // coherent speed (m/s)
```

Parse unit expressions:
```csharp
var registry = Timtek.SemanticQuantities.Quantities.DimensionSystem.Registry;
if (Timtek.SemanticQuantities.Registry.UnitExpressionParser.TryParse("m/s^2", registry, out var unit) && unit != null)
{
    var a = new Timtek.SemanticQuantities.Quantities.Quantity(9.80665, unit); // acceleration
}
```
