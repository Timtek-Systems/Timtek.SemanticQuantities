using Timtek.SemanticQuantities.Dimensions;

namespace Timtek.SemanticQuantities.Units;

// Frequency (1/s)
public sealed class Hertz : IUnit
{
    public string Name => "Hertz";
    public string Symbol => "Hz";
    public DimensionSignature Signature => new(0,0,-1,0,0,0,0,0);
    public double ToSI(double value) => value;
    public double FromSI(double siValue) => siValue;
}

// Pressure (N/m^2 = kg/(m*s^2))
public sealed class Pascal : IUnit
{
    public string Name => "Pascal";
    public string Symbol => "Pa";
    public DimensionSignature Signature => new(-1,1,-2,0,0,0,0,0);
    public double ToSI(double value) => value;
    public double FromSI(double siValue) => siValue;
}

// Energy (N*m = kg*m^2/s^2)
public sealed class Joule : IUnit
{
    public string Name => "Joule";
    public string Symbol => "J";
    public DimensionSignature Signature => new(2,1,-2,0,0,0,0,0);
    public double ToSI(double value) => value;
    public double FromSI(double siValue) => siValue;
}

// Power (J/s = kg*m^2/s^3)
public sealed class Watt : IUnit
{
    public string Name => "Watt";
    public string Symbol => "W";
    public DimensionSignature Signature => new(2,1,-3,0,0,0,0,0);
    public double ToSI(double value) => value;
    public double FromSI(double siValue) => siValue;
}

// Electric charge (A*s)
public sealed class Coulomb : IUnit
{
    public string Name => "Coulomb";
    public string Symbol => "C";
    public DimensionSignature Signature => new(0,0,1,1,0,0,0,0);
    public double ToSI(double value) => value;
    public double FromSI(double siValue) => siValue;
}

// Electric potential (W/A)
public sealed class Volt : IUnit
{
    public string Name => "Volt";
    public string Symbol => "V";
    public DimensionSignature Signature => new(2,1,-3,-1,0,0,0,0);
    public double ToSI(double value) => value;
    public double FromSI(double siValue) => siValue;
}

// Resistance (V/A)
public sealed class Ohm : IUnit
{
    public string Name => "Ohm";
    public string Symbol => "Ω";
    public DimensionSignature Signature => new(2,1,-3,-2,0,0,0,0);
    public double ToSI(double value) => value;
    public double FromSI(double siValue) => siValue;
}

// Conductance (A/V)
public sealed class Siemens : IUnit
{
    public string Name => "Siemens";
    public string Symbol => "S";
    public DimensionSignature Signature => new(-2,-1,3,2,0,0,0,0);
    public double ToSI(double value) => value;
    public double FromSI(double siValue) => siValue;
}

// Capacitance (C/V)
public sealed class Farad : IUnit
{
    public string Name => "Farad";
    public string Symbol => "F";
    public DimensionSignature Signature => new(-2,-1,4,2,0,0,0,0);
    public double ToSI(double value) => value;
    public double FromSI(double siValue) => siValue;
}

// Magnetic flux (V*s)
public sealed class Weber : IUnit
{
    public string Name => "Weber";
    public string Symbol => "Wb";
    public DimensionSignature Signature => new(2,1,-2,-1,0,0,0,0);
    public double ToSI(double value) => value;
    public double FromSI(double siValue) => siValue;
}

// Magnetic flux density (Wb/m^2)
public sealed class Tesla : IUnit
{
    public string Name => "Tesla";
    public string Symbol => "T";
    public DimensionSignature Signature => new(0,1,-2,-1,0,0,0,0);
    public double ToSI(double value) => value;
    public double FromSI(double siValue) => siValue;
}

// Inductance (V*s/A)
public sealed class Henry : IUnit
{
    public string Name => "Henry";
    public string Symbol => "H";
    public DimensionSignature Signature => new(2,1,-2,-2,0,0,0,0);
    public double ToSI(double value) => value;
    public double FromSI(double siValue) => siValue;
}

// Solid angle (dimensionless in SI canon), but we track angle separately; sr is dimensionless here.
public sealed class Steradian : IUnit
{
    public string Name => "Steradian";
    public string Symbol => "sr";
    public DimensionSignature Signature => DimensionSignature.Dimensionless;
    public double ToSI(double value) => value;
    public double FromSI(double siValue) => siValue;
}

// Luminous flux (lm = cd*sr)
public sealed class Lumen : IUnit
{
    public string Name => "Lumen";
    public string Symbol => "lm";
    public DimensionSignature Signature => DimensionSignature.LuminousIntensity; // cd*sr -> cd (sr is dimensionless)
    public double ToSI(double value) => value;
    public double FromSI(double siValue) => siValue;
}

// Illuminance (lx = lm/m^2 = cd/m^2)
public sealed class Lux : IUnit
{
    public string Name => "Lux";
    public string Symbol => "lx";
    public DimensionSignature Signature => new(-2,0,0,0,0,0,1,0);
    public double ToSI(double value) => value;
    public double FromSI(double siValue) => siValue;
}
