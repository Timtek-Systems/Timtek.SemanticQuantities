# Dimensions

## DimensionSignature
Namespace: Timtek.SemanticQuantities.Dimensions

Represents the dimensional exponents in the order:
L (length), M (mass), T (time), I (electric current), Θ (temperature), N (amount), J (luminous intensity), Angle (angular).

Key members:
- Predefined signatures: Dimensionless, Length, Mass, Time, ElectricCurrent, Temperature, Amount, LuminousIntensity, Angular
- Operators + and - for composing signatures
- Equality, hashing, and a compact ToString() like "[1,0,-1,0,0,0,0,0]"

Example:
```csharp
var length = Timtek.SemanticQuantities.Dimensions.DimensionSignature.Length;      // L^1
var time   = Timtek.SemanticQuantities.Dimensions.DimensionSignature.Time;        // T^1
var speed  = length - time; // L^1 T^-1
```

Angle as a base dimension: This avoids conflating angles with pure scalars and supports safer arithmetic in astronomy and mechanics.
