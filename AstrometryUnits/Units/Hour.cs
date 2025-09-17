public class Hour : IUnit
{
    public double ToSI(double value) => value * 3600.0;
    public double FromSI(double siValue) => siValue / 3600.0;
}
