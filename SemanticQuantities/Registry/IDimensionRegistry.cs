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
    /// Returns the preferred coherent SI unit for the given signature. If none registered,
    /// returns a canonical CompoundUnit constructed from the SI base exponents.
    /// </summary>
    IUnit GetCoherentUnit(DimensionSignature signature);

    /// <summary>
    /// Registers a unit. Implementations may be mutable during bootstrap and immutable after freeze.
    /// </summary>
    void Register(IUnit unit);

    /// <summary>
    /// Converts a quantity to the specified unit, verifying dimension compatibility.
    /// </summary>
    Quantity Convert(Quantity source, IUnit target);
}
