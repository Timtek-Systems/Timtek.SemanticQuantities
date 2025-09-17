public sealed class BasicTimeScaleContext : ITimeScaleContext
{
    public ILeapSecondsProvider LeapSeconds { get; }
    public IEopProvider? Eop { get; }
    public IEphemerisProvider? Ephemeris { get; }

    public BasicTimeScaleContext(ILeapSecondsProvider? leapSeconds = null, IEopProvider? eop = null, IEphemerisProvider? ephemeris = null)
    {
        LeapSeconds = leapSeconds ?? new BasicLeapSecondsProvider();
        Eop = eop;
        Ephemeris = ephemeris;
    }
}
