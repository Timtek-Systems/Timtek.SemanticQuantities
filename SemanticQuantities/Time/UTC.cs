public sealed class UTC : ITimeScale
{
    public string Name => "UTC";

    private static readonly DateTime EpochUtc = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public double ToTaiSeconds(double secondsSinceEpochInThisScale, ITimeScaleContext ctx)
    {
        var utc = EpochUtc.AddSeconds(secondsSinceEpochInThisScale);
        var offset = ctx.LeapSeconds.TaiMinusUtcSeconds(utc);
        return secondsSinceEpochInThisScale + offset;
    }

    public double FromTaiSeconds(double taiSecondsSinceEpoch, ITimeScaleContext ctx)
    {
        // Simple fixed-point iteration; adequate for a stub provider without steps.
        double guessUtc = taiSecondsSinceEpoch;
        for (int i = 0; i < 3; i++)
        {
            var utc = EpochUtc.AddSeconds(guessUtc);
            var off = ctx.LeapSeconds.TaiMinusUtcSeconds(utc);
            var f = guessUtc + off - taiSecondsSinceEpoch;
            guessUtc -= f; // df/dx ≈ 1
        }
        return guessUtc;
    }
}
