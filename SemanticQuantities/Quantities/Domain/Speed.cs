public static class Speed
{
    public static Quantity<MeterPerSecond> FromMetersPerSecond(double   mps) => new(mps, true);
    public static Quantity<MeterPerSecond> FromKilometersPerHour(double kmh) => new(kmh * 1000.0 / 3600.0);
}