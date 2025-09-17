namespace Timtek.SemanticQuantities.Time;

public interface ILeapSecondsProvider
{
    // Returns TAI - UTC at the given UTC date/time, in seconds.
    double TaiMinusUtcSeconds(DateTime utc);
}