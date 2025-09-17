using System;

public class BasicLeapSecondsProvider : ILeapSecondsProvider
{
    // Minimal stub: returns 0 for all dates. Replace with a real leap second table for accuracy.
    public double TaiMinusUtcSeconds(DateTime utc) => 0.0;
}
