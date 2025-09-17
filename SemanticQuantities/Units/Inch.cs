using Timtek.SemanticQuantities.Dimensions;

namespace Timtek.SemanticQuantities.Units;

public sealed class Inch : IUnit
{
    public string Name => "Inch";
    public string Symbol => "in";
    public DimensionSignature Signature => DimensionSignature.Length;
    public double ToSI(double value) => value * 0.0254;
    public double FromSI(double siValue) => siValue / 0.0254;
}
