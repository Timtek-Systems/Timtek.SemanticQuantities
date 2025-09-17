namespace Timtek.SemanticQuantities.Time;

public interface IEopProvider
{
    // Returns DUT1 = UT1 - UTC in seconds for the given UTC instant (if available).
    double Dut1Seconds(DateTime utc);
}