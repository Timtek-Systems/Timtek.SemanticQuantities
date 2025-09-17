
namespace Timtek.SemanticQuantities.Units;

public class Kilometer : IUnit
{
    public double ToSI(double value) => value * 1000.0;
    public double FromSI(double siValue) => siValue / 1000.0;
}