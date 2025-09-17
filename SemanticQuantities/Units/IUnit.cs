
namespace Timtek.SemanticQuantities.Units;

public interface IUnit
{
    double ToSI(double value);
    double FromSI(double siValue);
}