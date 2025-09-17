public sealed class TT : ITimeScale
{
    public string Name => "TT";
    private const double Offset = 32.184; // seconds
    public double ToTaiSeconds(double secondsSinceEpochInThisScale, ITimeScaleContext ctx) => secondsSinceEpochInThisScale - Offset;
    public double FromTaiSeconds(double taiSecondsSinceEpoch, ITimeScaleContext ctx) => taiSecondsSinceEpoch + Offset;
}
