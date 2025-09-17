namespace Timtek.SemanticQuantities.Units;

public class JulianDay : IUnit
{
    public double ToSI(double value) => value * 86400.0;
    public double FromSI(double siValue) => siValue / 86400.0;
}