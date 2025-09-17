using Timtek.SemanticQuantities.Dimensions;

namespace Timtek.SemanticQuantities.Units;

public sealed class Hour : IUnit
{
    public string             Name      => "Hour";
    public string             Symbol    => "h";
    public DimensionSignature Signature => DimensionSignature.Time;

    public double ToSI(double value) => value * 3600.0;
    public double FromSI(double siValue) => siValue / 3600.0;
}
