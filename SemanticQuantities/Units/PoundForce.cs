using Timtek.SemanticQuantities.Dimensions;

namespace Timtek.SemanticQuantities.Units;

public sealed class PoundForce : IUnit
{
    public string Name => "Pound-Force";
    public string Symbol => "lbf";
    public DimensionSignature Signature => new(1, 1, -2, 0, 0, 0, 0, 0);

    private const double N_PER_LBF = 4.4482216152605; // exact by definition

    public double ToSI(double value) => value * N_PER_LBF; // -> N
    public double FromSI(double siValue) => siValue / N_PER_LBF; // N -> lbf
}
