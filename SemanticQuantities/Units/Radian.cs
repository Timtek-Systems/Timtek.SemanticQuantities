namespace Timtek.SemanticQuantities.Units;

public class Radian : IUnit
{
    public double ToSI(double   value) => value;
    public double FromSI(double siValue) => siValue;
}