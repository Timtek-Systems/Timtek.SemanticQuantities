using System;
using Timtek.SemanticQuantities.Quantities.Domain;
using static System.Math;

namespace TA.ObjectOrientedAstronomy.Observatory
    {
    /// <summary>
    /// Calculates the optimum dome position (azimuth and elevation) for a given mount geometry and telescope position.
    /// </summary>
    /// <remarks>
    /// See "Dome Predictions for an Equatorial Telescope"  at http://www.tpointsw.uk/edome.pdf
    /// by Patrick T. Wallace, Tpoint Consulting {tpw@tpointsw.uk} [Wallace].
    /// </remarks>
    public class WallaceDomeSync
        {
        private readonly ObservatoryGeometry geometry;

        public WallaceDomeSync(ObservatoryGeometry geometry)
            {
            this.geometry = geometry;
            }

public DomePosition FromTelescopeMechanicalPosition(HourAngle mechanicalHourAngle, Declination mechanicalDeclination)
            {
            return FromTelescopeMechanicalPositionRadians(mechanicalHourAngle.InRadians(), mechanicalDeclination.InRadians());
            }

        /// <summary>
        /// Computes the dome position from raw mechanical hour angle and declination in radians, per [Wallace].
        /// This overload avoids clamping that may be enforced by value types intended for celestial declination.
        /// </summary>
        /// <param name="mechanicalHourAngleRadians">Mechanical hour angle h, in radians.</param>
        /// <param name="mechanicalDeclinationRadians">Mechanical declination δ, in radians. May lie outside [-π/2, +π/2].</param>
public DomePosition FromTelescopeMechanicalPositionRadians(double mechanicalHourAngleRadians, double mechanicalDeclinationRadians)
            {
            // Unpack the various inputs so that we have names matching the equations in [Wallace].
            var h = mechanicalHourAngleRadians;
            var δ = mechanicalDeclinationRadians;
            var p = geometry.PolarDeclinationAxisDistance.ValueSI;
            var q = geometry.PolarOpticalAxisDistance.ValueSI;
            var r = geometry.DeclinationOpticalAxisDistance.ValueSI;
            var φ = geometry.ObservatoryLatitude.InRadians();
            var rD = geometry.DomeRadius.ValueSI;
            var xm = geometry.MountOffsetEast.ValueSI;
            var ym = geometry.MountOffsetNorth.ValueSI;
            var zm = geometry.MountOffsetUp.ValueSI;

            // Runtime guards: finite inputs and positive dome radius.
            if (double.IsNaN(h) || double.IsInfinity(h)) throw new ArgumentOutOfRangeException(nameof(mechanicalHourAngleRadians), h, "Hour angle must be finite");
            if (double.IsNaN(δ) || double.IsInfinity(δ)) throw new ArgumentOutOfRangeException(nameof(mechanicalDeclinationRadians), δ, "Declination must be finite");
            if (double.IsNaN(rD) || double.IsInfinity(rD) || rD <= 0) throw new ArgumentOutOfRangeException(nameof(geometry.DomeRadius), rD, "Dome radius must be a positive finite value (meters)");
            if (double.IsNaN(xm) || double.IsInfinity(xm)) throw new ArgumentOutOfRangeException(nameof(geometry.MountOffsetEast), xm, "Mount offset east must be finite");
            if (double.IsNaN(ym) || double.IsInfinity(ym)) throw new ArgumentOutOfRangeException(nameof(geometry.MountOffsetNorth), ym, "Mount offset north must be finite");
            if (double.IsNaN(zm) || double.IsInfinity(zm)) throw new ArgumentOutOfRangeException(nameof(geometry.MountOffsetUp), zm, "Mount offset up must be finite");
            if (double.IsNaN(p) || double.IsInfinity(p)) throw new ArgumentOutOfRangeException(nameof(geometry.PolarDeclinationAxisDistance), p, "p must be finite");
            if (double.IsNaN(q) || double.IsInfinity(q)) throw new ArgumentOutOfRangeException(nameof(geometry.PolarOpticalAxisDistance), q, "q must be finite");
            if (double.IsNaN(r) || double.IsInfinity(r)) throw new ArgumentOutOfRangeException(nameof(geometry.DeclinationOpticalAxisDistance), r, "r must be finite");
            if (double.IsNaN(φ) || double.IsInfinity(φ)) throw new ArgumentOutOfRangeException(nameof(geometry.ObservatoryLatitude), φ, "Latitude must be finite");

            // Numbers in braces () in the comments refer to equations in [Wallace].
            // Calculate vector mount to optical center
            var y = p + r * Sin(δ);             // (1)
            var xmo = q * Cos(h) + y * Sin(h);  // (2)
            var ymo = -q * Sin(h) + y * Cos(h); // (3)
            var zmo = r * Cos(δ);               // (4)

            // Calculate vector dome to optical center in east-north-up frame
            var xdo = xm + xmo;                         // (5)
            var ydo = ym + ymo * Sin(φ) + zmo * Cos(φ); // (6)
            var zdo = zm - ymo * Cos(φ) + zmo * Sin(φ); // (7)

            // Calculate the telescope (A,E) unit vector in the east-north-up frame
            var x1 = -Sin(h) * Cos(δ);              // (8)
            var y1 = -Cos(h) * Cos(δ);              // (9)
            var z1 = Sin(δ);                        // (10)
            var xs = x1;                            // (11)
            var ys = y1 * Sin(φ) + z1 * Cos(φ);     // (12)
            var zs = -y1 * Cos(φ) + z1 * Sin(φ);    // (13)

            // Solve for the distance from the optical centre to the dome aperture
            var sdt = xs * xdo + ys * ydo + zs * zdo;       // (14)
            var t2m = xdo * xdo + ydo * ydo + zdo * zdo;    // (15)
            var w = sdt * sdt - t2m + rD * rD;              // (16)
            if (w < 0)
                throw new InvalidOperationException($"No intersection with dome (w={w}). Inputs: h={h}, δ={δ}, rD={rD}");
            var f = -sdt + Sqrt(w);                         // (17)

            // Calculate vector dome centre to dome aperture
            var xda = xdo + f * xs; // (18)
            var yda = ydo + f * ys; // (19)
            var zda = zdo + f * zs; // (20)

            // Convert to spherical coordinates (in radians)
            var A = Atan2(xda, yda);                            // (21)
            var E = Atan2(zda, Sqrt(xda * xda + yda * yda));    // (22)

            // Normalize azimuth to [0, 2π) to match [Wallace] presentation.
            if (A < 0) A += 2.0 * PI;

            // Package and return the result.
            var domePosition = DomePosition.FromRadians(A, E);
            return domePosition;
            }
        }
    }
