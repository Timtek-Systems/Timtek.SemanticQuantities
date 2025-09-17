namespace Timtek.SemanticQuantities.Units;

public class Degree : IUnit
{
    public double ToSI(double   value) => value * Math.PI / 180.0;
    public double FromSI(double siValue) => siValue * 180.0 / Math.PI;
}