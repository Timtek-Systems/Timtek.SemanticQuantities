using System;

public static class Speed
{
    public static Quantity<MeterPerSecond> FromMetersPerSecond(double mps) => new Quantity<MeterPerSecond>(mps, isSI: true);
    public static Quantity<MeterPerSecond> FromKilometersPerHour(double kmh) => new Quantity<MeterPerSecond>(kmh * 1000.0 / 3600.0);
}