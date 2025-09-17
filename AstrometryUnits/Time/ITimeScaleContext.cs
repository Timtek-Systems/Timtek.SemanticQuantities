public interface ITimeScaleContext
{
    ILeapSecondsProvider LeapSeconds { get; }
    IEopProvider? Eop { get; }
    IEphemerisProvider? Ephemeris { get; }
}
