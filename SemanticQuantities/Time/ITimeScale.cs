public interface ITimeScale
{
    string Name { get; }
    double ToTaiSeconds(double secondsSinceEpochInThisScale, ITimeScaleContext ctx);
    double FromTaiSeconds(double taiSecondsSinceEpoch, ITimeScaleContext ctx);
}
