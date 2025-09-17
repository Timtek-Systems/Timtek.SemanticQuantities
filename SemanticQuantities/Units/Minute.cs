namespace Timtek.SemanticQuantities.Units;

public class Minute : IUnit
{
    public double ToSI(double   value) => value * 60.0;
    public double FromSI(double siValue) => siValue / 60.0;
}