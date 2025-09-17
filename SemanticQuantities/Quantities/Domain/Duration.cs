public static class Duration
{
    public static Quantity<Second> FromSeconds(double    seconds) => new(seconds, true);
    public static Quantity<Second> FromMinutes(double    minutes) => new(minutes * 60.0);
    public static Quantity<Second> FromHours(double      hours) => new(hours * 3600.0);
    public static Quantity<Second> FromDays(double       days) => new(days * 86400.0);
    public static Quantity<Second> FromJulianDays(double julianDays) => new(julianDays * 86400.0);
}
