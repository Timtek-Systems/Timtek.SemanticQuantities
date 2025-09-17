using Timtek.SemanticQuantities.Dimensions;
using Timtek.SemanticQuantities.Units;
using Timtek.SemanticQuantities.Registry;

namespace Timtek.SemanticQuantities.Quantities;

/// <summary>
/// Runtime quantity carrying a value in coherent SI and a declared display unit.
/// Operators are dimension-aware and results are expressed in coherent SI units via a registry.
/// </summary>
public readonly struct Quantity
{
    public Quantity(double value, IUnit unit)
    {
        Unit = unit ?? throw new ArgumentNullException(nameof(unit));
        ValueSI = unit.ToSI(value);
    }

    public Quantity(double valueSI, IUnit unit, bool isSI)
    {
        Unit = unit ?? throw new ArgumentNullException(nameof(unit));
        ValueSI = isSI ? valueSI : unit.ToSI(valueSI);
    }

    /// <summary>
    /// Value in coherent SI units.
    /// </summary>
    public double ValueSI { get; }

    /// <summary>
    /// Declared/display unit for this quantity. Conversion to SI is defined by this unit.
    /// </summary>
    public IUnit Unit { get; }

    /// <summary>
    /// Dimension of the quantity as defined by Unit.Signature.
    /// </summary>
    public DimensionSignature Signature => Unit.Signature;

    /// <summary>
    /// Returns the numeric value in the requested unit (dimension must match).
    /// </summary>
    public double As(IUnit target)
    {
        if (target.Signature.Equals(Signature)) return target.FromSI(ValueSI);
        throw new InvalidOperationException($"Dimension mismatch: {Signature} vs {target.Signature}");
    }

    /// <summary>
    /// Converts this quantity to an equivalent quantity with the specified unit.
    /// </summary>
    public Quantity To(IUnit target) => new(target.FromSI(ValueSI), target);

    /// <summary>
    /// Returns a new quantity expressed in the registry's preferred coherent SI unit for this dimension.
    /// </summary>
    public Quantity Normalize()
    {
        var coherent = DimensionSystem.Registry.GetCoherentUnit(Signature);
        return new Quantity(coherent.FromSI(ValueSI), coherent);
    }

    public override string ToString() => $"{Unit.FromSI(ValueSI)} {Unit.Symbol}";

    // Arithmetic
    public static Quantity operator +(Quantity a, Quantity b) =>
        AddSub(a, b, +1);

    public static Quantity operator -(Quantity a, Quantity b) =>
        AddSub(a, b, -1);

    public static Quantity operator -(Quantity a) => new(-a.Unit.FromSI(a.ValueSI), a.Unit);

    public static Quantity operator *(Quantity a, Quantity b) =>
        MulDiv(a, b, isMultiply: true);

    public static Quantity operator /(Quantity a, Quantity b) =>
        MulDiv(a, b, isMultiply: false);

    public static Quantity operator *(Quantity a, double k) => new(a.Unit.FromSI(a.ValueSI) * k, a.Unit);
    public static Quantity operator *(double k, Quantity a) => a * k;
    public static Quantity operator /(Quantity a, double k) => new(a.Unit.FromSI(a.ValueSI) / k, a.Unit);

    private static Quantity AddSub(Quantity a, Quantity b, int sign)
    {
        if (!a.Signature.Equals(b.Signature))
            throw new InvalidOperationException($"Cannot {(sign > 0 ? "add" : "subtract")} quantities of different dimensions: {a.Signature} and {b.Signature}");
        var registry = DimensionSystem.Registry;
        var sysA = registry.GetUnitSystem(a.Unit);
        var sysB = registry.GetUnitSystem(b.Unit);
        var preferredSystem = sysA == sysB ? sysA : Registry.UnitSystem.SI; // default SI for mixed systems
        var coherent = registry.GetCoherentUnit(a.Signature, preferredSystem);
        var resultSi = a.ValueSI + sign * b.ValueSI;
        var valueInCoherent = coherent.FromSI(resultSi);
        return new Quantity(valueInCoherent, coherent);
    }

    private static Quantity MulDiv(Quantity a, Quantity b, bool isMultiply)
    {
        var signature = isMultiply ? a.Signature + b.Signature : a.Signature - b.Signature;
        var registry  = DimensionSystem.Registry;
        var sysA = registry.GetUnitSystem(a.Unit);
        var sysB = registry.GetUnitSystem(b.Unit);
        var preferredSystem = sysA == sysB ? sysA : Registry.UnitSystem.SI; // default SI for mixed systems
        var coherent  = registry.GetCoherentUnit(signature, preferredSystem);
        var resultSi  = isMultiply ? a.ValueSI * b.ValueSI : a.ValueSI / b.ValueSI;
        var valueInCoherent = coherent.FromSI(resultSi);
        return new Quantity(valueInCoherent, coherent);
    }
}
