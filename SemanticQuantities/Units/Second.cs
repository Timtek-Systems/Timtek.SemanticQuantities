namespace Timtek.SemanticQuantities.Units;

public class Second : IUnit
{
    public double ToSI(double value) => value;
    public double FromSI(double siValue) => siValue;
}