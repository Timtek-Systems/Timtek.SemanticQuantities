using Timtek.SemanticQuantities.Units;

namespace Timtek.SemanticQuantities.Quantities.Domain;

/// <summary>
/// Provides factories for creating distances using SI units of meters.
/// </summary>
public static class Distance
{
    /// <summary>
    /// Creates a distance from meters (SI). The internal representation is SI, so this is an identity construction.
    /// </summary>
    /// <param name="meters">Distance in meters.</param>
    /// <returns>A quantity of type <see cref="Meter"/> with the specified value in meters.</returns>
    public static Quantity<Meter> FromMeters(double meters) => new(meters, true);

    /// <summary>
    /// Creates a distance from kilometers.
    /// </summary>
    /// <param name="kilometers">Distance in kilometers.</param>
    /// <returns>A quantity of type <see cref="Meter"/> converted from kilometers.</returns>
    public static Quantity<Meter> FromKilometers(double kilometers) => new(kilometers * 1000.0);
}
