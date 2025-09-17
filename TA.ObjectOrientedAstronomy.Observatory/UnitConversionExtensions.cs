using System;
using System.Collections.Generic;
using System.Text;
using Timtek.SemanticQuantities.Quantities.Domain;

namespace TA.ObjectOrientedAstronomy.Observatory
    {
    internal static class UnitConversionExtensions
        {
        public static double DegreesToRadians(double degrees) => Math.PI * degrees / 180.0;

        public static double RadiansToDegrees(double radians) => radians * 180.0 / Math.PI;

public static double InDegrees(this HourAngleQuantity ha)
            {
            return ha.AsDegrees();
            }

        public static double InRadians(this HourAngleQuantity ha)
            {
            return ha.AsRadians();
            }

        public static double InRadians(this Declination dec)
            {
            return dec.AsRadians();
            }

        public static double InRadians(this Latitude lat)
            {
            return lat.AsRadians();
            }

        public static double AngularDistanceTo(this TelescopeMechanicalPosition from, TelescopeMechanicalPosition to)
            {
            var haDistance = to.HourAngle.InDegrees() - from.HourAngle.InDegrees();
var decDistance = to.Declination.AsDegrees() - from.Declination.AsDegrees();
            var vectorSum = HypotenuseDistance(haDistance, decDistance);
            return vectorSum;
            }

        private static double HypotenuseDistance(double opposite, double adjacent)
            {
            return Math.Sqrt(opposite * opposite + adjacent * adjacent);
            }
        }
    }