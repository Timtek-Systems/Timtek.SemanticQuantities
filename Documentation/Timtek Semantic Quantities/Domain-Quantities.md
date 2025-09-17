# Domain Quantities and Helpers

## Distance
- `Distance.FromMeters(double)`
- `Distance.FromKilometers(double)`

## Angle
- `Angle.FromDegrees(double)`
- `Angle.FromRadians(double)`

## Duration
- `Duration.FromSeconds(double)`, `Duration.FromMinutes(double)`, `Duration.FromHours(double)`, `Duration.FromDays(double)`, `Duration.FromJulianDays(double)`

## Speed
- `Speed.FromMetersPerSecond(double)`, `Speed.FromKilometersPerHour(double)`

## Direction and Velocity
- `Direction.FromXYZ(x, y, z)` normalizes to a unit vector
- `Velocity.From(Quantity<​MeterPerSecond> speed, Direction direction)`
- `Velocity.Vx` / `Velocity.Vy` / `Velocity.Vz` project components

## Mechanics
- `Mass.FromKilograms(double)`
- `Acceleration.FromMetersPerSecondSquared(double)`
- `Force.FromNewtons(double)`

## Examples
```csharp
// Distance and speed
var d = Timtek.SemanticQuantities.Quantities.Domain.Distance.FromKilometers(1.2);
var t = Timtek.SemanticQuantities.Quantities.Domain.Duration.FromMinutes(2);
var v = d / t; // m/s

// Direction + speed => velocity components
var dir = Timtek.SemanticQuantities.Quantities.Domain.Direction.FromXYZ(1, 1, 0);
var spd = Timtek.SemanticQuantities.Quantities.Domain.Speed.FromMetersPerSecond(10);
var vel = Timtek.SemanticQuantities.Quantities.Domain.Velocity.From(new Timtek.SemanticQuantities.Quantities.Quantity\<Timtek.SemanticQuantities.Units.MeterPerSecond\>(10), dir);
var vx = vel.Vx; // SI units

// Mechanics factories
var m   = Timtek.SemanticQuantities.Quantities.Domain.Mass.FromKilograms(2);
var a   = Timtek.SemanticQuantities.Quantities.Domain.Acceleration.FromMetersPerSecondSquared(9.81);
var f   = m * a; // N (via dimensional arithmetic)
```
