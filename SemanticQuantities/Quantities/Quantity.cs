using Timtek.SemanticQuantities.Units;

namespace Timtek.SemanticQuantities.Quantities;

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

    public override string ToString() => $"{ValueSI} SI";
}