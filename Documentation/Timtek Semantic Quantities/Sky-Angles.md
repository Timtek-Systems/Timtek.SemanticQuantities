# Sky Angles

## RightAscension
- Stored in radians normalized to [0, 2π)
- `FromHours`, `FromDegrees`, `FromRadians`
- `AsHours`, `AsDegrees`, `AsRadians`

## HourAngle
- Stored in radians normalized to [0, 2π)
- `FromHours`, `FromDegrees`, `FromRadians`
- `AsHoursUnwrapped` (0 to 24), `AsSignedHours` (-12 to +12)

## Declination
- Stored in radians, clamped to [-π/2, +π/2]
- `FromDegrees`, `FromRadians`
- `AsDegrees`, `AsRadians`

## Latitude
- Stored in radians, clamped to [-π/2, +π/2]
- `FromDegrees`, `FromRadians`

## Longitude
- Stored in radians, normalized to [-π, +π)
- `FromDegrees`, `FromRadians`
- `AsDegreesZeroTo360` for [0°, 360°)

### Examples
```csharp
var ra  = Timtek.SemanticQuantities.Quantities.Domain.RightAscension.FromHours(26.5); // wraps to [0,24)
var ha  = Timtek.SemanticQuantities.Quantities.Domain.HourAngle.FromDegrees(-200);    // wraps to [0,360)
var dec = Timtek.SemanticQuantities.Quantities.Domain.Declination.FromDegrees(95);    // clamps to +90
var lat = Timtek.SemanticQuantities.Quantities.Domain.Latitude.FromDegrees(-123);     // clamps to -90
var lon = Timtek.SemanticQuantities.Quantities.Domain.Longitude.FromDegrees(200);     // wraps to [-180, +180)
```
