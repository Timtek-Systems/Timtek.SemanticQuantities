using Timtek.SemanticQuantities.Units;

namespace Timtek.SemanticQuantities.Quantities;

public readonly struct Quantity<TUnit> where TUnit : IUnit, new()
{
    private readonly double _valueSI;

    public Quantity(double value, bool isSI = false)
    {
        var unit = new TUnit();
        _valueSI = isSI ? value : unit.ToSI(value);
    }

    public double As<TTargetUnit>() where TTargetUnit : IUnit, new()
    {
        var target = new TTargetUnit();
        return target.FromSI(_valueSI);
    }

    public double ValueSI => _valueSI;

    public override string ToString() => $"{_valueSI} SI";
}