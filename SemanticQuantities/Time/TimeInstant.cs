public readonly struct TimeInstant<TScale> where TScale : ITimeScale, new()
{
    private readonly double _taiSecondsSinceEpoch;
    private static readonly TScale Scale = new();

    public TimeInstant(double taiSecondsSinceEpoch)
    {
        _taiSecondsSinceEpoch = taiSecondsSinceEpoch;
    }

    public static TimeInstant<TScale> FromSeconds(double secondsSinceEpochInThisScale, ITimeScaleContext ctx)
        => new(Scale.ToTaiSeconds(secondsSinceEpochInThisScale, ctx));

    public double AsSeconds<TOther>(ITimeScaleContext ctx) where TOther : ITimeScale, new()
        => new TOther().FromTaiSeconds(_taiSecondsSinceEpoch, ctx);

    public TimeInstant<TScale> Add(Quantity<Second> duration)
        => new(_taiSecondsSinceEpoch + duration.ValueSI);

    public Quantity<Second> Subtract(TimeInstant<TScale> other) =>
        new(_taiSecondsSinceEpoch - other._taiSecondsSinceEpoch, true);
}
