using Timtek.SemanticQuantities.Dimensions;
using Timtek.SemanticQuantities.Units;
using Timtek.SemanticQuantities.Quantities;

namespace Timtek.SemanticQuantities.Registry;

public interface IDimensionRegistry
{
    /// <summary>
    /// Resolve a unit by symbol or name.
    /// </summary>
    IUnit ParseUnit(string text);

    /// <summary>Returns true if the symbol or name is known.</summary>
    bool IsKnownUnit(string text);

    /// <summary>
    /// Returns the preferred coherent unit for the given signature within the preferred system.
    /// If none registered in that system, falls back to SI. If still none, returns a canonical CompoundUnit.
    /// </summary>
    IUnit GetCoherentUnit(DimensionSignature signature, UnitSystem preferredSystem = UnitSystem.SI);

    /// <summary>
    /// Registers a unit with the specified system. Implementations may be mutable during bootstrap and immutable after freeze.
    /// </summary>
    void Register(IUnit unit, UnitSystem system);

    /// <summary>
    /// Convenience overload defaults to SI when unspecified.
    /// </summary>
    void Register(IUnit unit);

    /// <summary>
    /// Infers the unit system for a given unit type, defaulting to SI when unknown.
    /// </summary>
    UnitSystem GetUnitSystem(IUnit unit);

    /// <summary>
    /// Converts a quantity to the specified unit, verifying dimension compatibility.
    /// </summary>
    Quantity Convert(Quantity source, IUnit target);
}
