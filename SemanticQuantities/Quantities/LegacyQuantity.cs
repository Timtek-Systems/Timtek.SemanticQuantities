using Timtek.SemanticQuantities.Units;

namespace Timtek.SemanticQuantities.Quantities;

/// <summary>
/// Legacy generic Quantity retaining source compatibility while migrating to runtime Quantity.
/// Prefer using the non-generic Quantity going forward.
/// </summary>
[Obsolete("Use the non-generic Quantity (runtime) instead. This type remains for source compatibility during migration.")]
public readonly struct Quantity<TUnit> where TUnit : IUnit, new()
{
    public Quantity(double value, bool isSI = false)
    {
        var unit = new TUnit();
        ValueSI = isSI ? value : unit.ToSI(value);
    }

    public double As<TTargetUnit>() where TTargetUnit : IUnit, new()
    {
        var target = new TTargetUnit();
        return target.FromSI(ValueSI);
    }

    public double ValueSI { get; }

    public override string ToString()
    {
        // Best-effort formatting using the TUnit symbol if available
        var unit = new TUnit();
        return $"{unit.FromSI(ValueSI)} {unit.Symbol}";
    }
}
