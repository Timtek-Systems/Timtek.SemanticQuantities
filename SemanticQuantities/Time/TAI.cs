namespace Timtek.SemanticQuantities.Time;

public sealed class TAI : ITimeScale
{
    public string Name => "TAI";
    public double ToTaiSeconds(double   secondsSinceEpochInThisScale, ITimeScaleContext ctx) => secondsSinceEpochInThisScale;
    public double FromTaiSeconds(double taiSecondsSinceEpoch,         ITimeScaleContext ctx) => taiSecondsSinceEpoch;
}