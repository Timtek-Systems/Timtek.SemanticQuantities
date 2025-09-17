using Timtek.SemanticQuantities.Dimensions;

namespace Timtek.SemanticQuantities.Units;

public sealed class Psi : IUnit
{
    public string Name => "Pound per Square Inch";
    public string Symbol => "psi";
    public DimensionSignature Signature => new(-1, 1, -2, 0, 0, 0, 0, 0); // pressure

    // 1 psi = 1 lbf/in^2 = 6894.757293168361 Pa
    private const double PA_PER_PSI = 6894.757293168361;

    public double ToSI(double value) => value * PA_PER_PSI; // -> Pa
    public double FromSI(double siValue) => siValue / PA_PER_PSI; // Pa -> psi
}
