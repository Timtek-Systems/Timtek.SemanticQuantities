namespace Timtek.SemanticQuantities.Time;

public sealed class TT : ITimeScale
{
    private const double Offset = 32.184; // seconds
    public string Name => "TT";
    public double ToTaiSeconds(double secondsSinceEpochInThisScale, ITimeScaleContext ctx) => secondsSinceEpochInThisScale - Offset;
    public double FromTaiSeconds(double taiSecondsSinceEpoch, ITimeScaleContext ctx) => taiSecondsSinceEpoch + Offset;
}