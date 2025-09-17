public static class Duration
{
    public static Quantity<Second> FromSeconds(double seconds) => new Quantity<Second>(seconds, isSI: true);
    public static Quantity<Second> FromMinutes(double minutes) => new Quantity<Second>(minutes * 60.0);
    public static Quantity<Second> FromHours(double hours) => new Quantity<Second>(hours * 3600.0);
    public static Quantity<Second> FromDays(double days) => new Quantity<Second>(days * 86400.0);
    public static Quantity<Second> FromJulianDays(double julianDays) => new Quantity<Second>(julianDays * 86400.0);
}
