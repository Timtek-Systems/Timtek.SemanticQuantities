namespace Timtek.SemanticQuantities.Time;

/// <summary>
///     Leap seconds provider based on the NIST UTC leap-seconds table (modern UTC, 1972 and later).
///     Returns TAI - UTC in seconds as a stepwise-constant function that changes only at defined leap-second instants.
/// </summary>
/// <remarks>
///     Source: NIST Time Realization – Leap Seconds table.
///     This provider does not support dates before 1972-01-01T00:00:00Z; for those, use
///     <see cref="NistHistoricalUtcProvider" />.
///     Additional future leap seconds can be supplied via constructor or <see cref="AddLeapSecond" />.
/// </remarks>
public sealed class NistLeapSecondsProvider : ILeapSecondsProvider
{
    private static readonly StepEntry[] BuiltInSteps = new[]
    {
        new StepEntry(new DateTime(1972, 1, 1, 0, 0, 0, DateTimeKind.Utc), 10),
        new StepEntry(new DateTime(1972, 7, 1, 0, 0, 0, DateTimeKind.Utc), 11),
        new StepEntry(new DateTime(1973, 1, 1, 0, 0, 0, DateTimeKind.Utc), 12),
        new StepEntry(new DateTime(1974, 1, 1, 0, 0, 0, DateTimeKind.Utc), 13),
        new StepEntry(new DateTime(1975, 1, 1, 0, 0, 0, DateTimeKind.Utc), 14),
        new StepEntry(new DateTime(1976, 1, 1, 0, 0, 0, DateTimeKind.Utc), 15),
        new StepEntry(new DateTime(1977, 1, 1, 0, 0, 0, DateTimeKind.Utc), 16),
        new StepEntry(new DateTime(1978, 1, 1, 0, 0, 0, DateTimeKind.Utc), 17),
        new StepEntry(new DateTime(1979, 1, 1, 0, 0, 0, DateTimeKind.Utc), 18),
        new StepEntry(new DateTime(1980, 1, 1, 0, 0, 0, DateTimeKind.Utc), 19),
        new StepEntry(new DateTime(1981, 7, 1, 0, 0, 0, DateTimeKind.Utc), 20),
        new StepEntry(new DateTime(1982, 7, 1, 0, 0, 0, DateTimeKind.Utc), 21),
        new StepEntry(new DateTime(1983, 7, 1, 0, 0, 0, DateTimeKind.Utc), 22),
        new StepEntry(new DateTime(1985, 7, 1, 0, 0, 0, DateTimeKind.Utc), 23),
        new StepEntry(new DateTime(1988, 1, 1, 0, 0, 0, DateTimeKind.Utc), 24),
        new StepEntry(new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc), 25),
        new StepEntry(new DateTime(1991, 1, 1, 0, 0, 0, DateTimeKind.Utc), 26),
        new StepEntry(new DateTime(1992, 7, 1, 0, 0, 0, DateTimeKind.Utc), 27),
        new StepEntry(new DateTime(1993, 7, 1, 0, 0, 0, DateTimeKind.Utc), 28),
        new StepEntry(new DateTime(1994, 7, 1, 0, 0, 0, DateTimeKind.Utc), 29),
        new StepEntry(new DateTime(1996, 1, 1, 0, 0, 0, DateTimeKind.Utc), 30),
        new StepEntry(new DateTime(1997, 7, 1, 0, 0, 0, DateTimeKind.Utc), 31),
        new StepEntry(new DateTime(1999, 1, 1, 0, 0, 0, DateTimeKind.Utc), 32),
        new StepEntry(new DateTime(2006, 1, 1, 0, 0, 0, DateTimeKind.Utc), 33),
        new StepEntry(new DateTime(2009, 1, 1, 0, 0, 0, DateTimeKind.Utc), 34),
        new StepEntry(new DateTime(2012, 7, 1, 0, 0, 0, DateTimeKind.Utc), 35),
        new StepEntry(new DateTime(2015, 7, 1, 0, 0, 0, DateTimeKind.Utc), 36),
        new StepEntry(new DateTime(2017, 1, 1, 0, 0, 0, DateTimeKind.Utc), 37)
    };

    private readonly List<StepEntry> _steps;

    /// <summary>
    ///     Constructs the provider with the built-in NIST step table.
    /// </summary>
    public NistLeapSecondsProvider() => _steps = new List<StepEntry>(BuiltInSteps);

    /// <summary>
    ///     Constructs the provider with the built-in steps plus additional user-supplied steps.
    /// </summary>
    public NistLeapSecondsProvider(IEnumerable<(DateTime effectiveFromUtc, int taiMinusUtcSeconds)> extraSteps)
        : this()
    {
        if (extraSteps == null) return;
        foreach (var (instant, offset) in extraSteps)
            AddLeapSecond(instant, offset);
    }

    public double TaiMinusUtcSeconds(DateTime utc)
    {
        if (utc.Kind != DateTimeKind.Utc)
            throw new ArgumentException("utc must be specified in UTC", nameof(utc));

        if (_steps.Count == 0)
            return 0.0;

        if (utc < _steps[0].EffectiveFromUtc)
            throw new NotSupportedException(
                "NistLeapSecondsProvider supports 1972-01-01 and later. Use NistHistoricalUtcProvider for earlier dates.");

        var ub = UpperBound(_steps, utc);
        var i  = ub - 1;
        if (i < 0) i = 0;
        return _steps[i].TaiMinusUtcSeconds;
    }

    /// <summary>
    ///     Adds a future leap-second step. The step must be in UTC and change the offset by exactly ±1 second
    ///     relative to the preceding step. Steps are kept sorted by their effective instant.
    /// </summary>
    public void AddLeapSecond(DateTime effectiveFromUtc, int taiMinusUtcSeconds)
    {
        if (effectiveFromUtc.Kind != DateTimeKind.Utc)
            throw new ArgumentException("effectiveFromUtc must be in UTC", nameof(effectiveFromUtc));

        // Insert or replace while keeping sort order
        var newEntry = new StepEntry(effectiveFromUtc, taiMinusUtcSeconds);

        // Find insertion index (first index with EffectiveFromUtc > new instant)
        var idx = UpperBound(_steps, effectiveFromUtc) - 1; // idx now last entry <= effectiveFromUtc

        if (_steps.Count > 0)
        {
            // Check uniqueness: if an entry already exists at the same instant, replace but validate jump size.
            var sameIdx = _steps.FindIndex(s => s.EffectiveFromUtc == effectiveFromUtc);
            if (sameIdx >= 0)
            {
                // Validate jump relative to previous (if any)
                var prev = sameIdx > 0 ? _steps[sameIdx - 1].TaiMinusUtcSeconds : _steps[sameIdx].TaiMinusUtcSeconds;
                if (Math.Abs(taiMinusUtcSeconds - prev) != 1)
                    throw new ArgumentException("Leap-second change must be exactly ±1 second relative to previous step.");
                _steps[sameIdx] = newEntry;
                return;
            }

            // Validate jump relative to previous (if any)
            var prevIdx = Math.Max(0, idx);
            var prevVal = _steps[prevIdx].TaiMinusUtcSeconds;
            if (Math.Abs(taiMinusUtcSeconds - prevVal) != 1)
                throw new ArgumentException("Leap-second change must be exactly ±1 second relative to previous step.");
        }

        // Insert in sorted order
        var insertAt = LowerBound(_steps, effectiveFromUtc);
        _steps.Insert(insertAt, newEntry);
    }

    private static int UpperBound(List<StepEntry> a, DateTime key)
    {
        int lo = 0, hi = a.Count;
        while (lo < hi)
        {
            var mid = lo + ((hi - lo) >> 1);
            if (a[mid].EffectiveFromUtc <= key) lo = mid + 1;
            else hi = mid;
        }

        return lo;
    }

    private static int LowerBound(List<StepEntry> a, DateTime key)
    {
        int lo = 0, hi = a.Count;
        while (lo < hi)
        {
            var mid = lo + ((hi - lo) >> 1);
            if (a[mid].EffectiveFromUtc < key) lo = mid + 1;
            else hi = mid;
        }

        return lo;
    }

    private struct StepEntry
    {
        public readonly DateTime EffectiveFromUtc; // inclusive
        public readonly int      TaiMinusUtcSeconds;

        public StepEntry(DateTime effectiveFromUtc, int taiMinusUtcSeconds)
        {
            EffectiveFromUtc = DateTime.SpecifyKind(effectiveFromUtc, DateTimeKind.Utc);
            TaiMinusUtcSeconds = taiMinusUtcSeconds;
        }
    }
}