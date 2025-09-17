using Timtek.SemanticQuantities.Units;

namespace Timtek.SemanticQuantities.Quantities.Domain;

public static class Duration
{
    public static Quantity FromSeconds(double    seconds) => new(seconds, new Second());
    public static Quantity FromMinutes(double    minutes) => new(minutes, new Minute());
    public static Quantity FromHours(double      hours) => new(hours, new Hour());
    public static Quantity FromDays(double       days) => new(days, new Day());
    public static Quantity FromJulianDays(double julianDays) => new(julianDays, new JulianDay());
}
