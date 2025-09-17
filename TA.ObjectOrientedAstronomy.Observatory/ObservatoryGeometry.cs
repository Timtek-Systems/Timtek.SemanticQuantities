using System;
using Timtek.SemanticQuantities.Quantities;
using Timtek.SemanticQuantities.Quantities.Domain;
using Timtek.SemanticQuantities.Units;

namespace TA.ObjectOrientedAstronomy.Observatory
    {
    /// <summary>
    ///     Describes an observatory's optical geometry for the purposes of calculating telescope-dome synchronization.
    ///     All distance/offset properties are represented as SI distances in meters via Quantity<Meter>.
    /// </summary>
public class ObservatoryGeometry
        {
        /// <summary>
        /// Creates a new observatory geometry with all parameters specified.
        /// </summary>
        public ObservatoryGeometry(
            Quantity mountOffsetEast,
            Quantity mountOffsetNorth,
            Quantity mountOffsetUp,
            Quantity domeRadius,
            Latitude observatoryLatitude,
            Quantity polarDeclinationAxisDistance,
            Quantity polarOpticalAxisDistance,
            Quantity declinationOpticalAxisDistance)
            {
            MountOffsetEast = mountOffsetEast;
            MountOffsetNorth = mountOffsetNorth;
            MountOffsetUp = mountOffsetUp;
            DomeRadius = domeRadius;
            ObservatoryLatitude = observatoryLatitude;
            PolarDeclinationAxisDistance = polarDeclinationAxisDistance;
            PolarOpticalAxisDistance = polarOpticalAxisDistance;
            DeclinationOpticalAxisDistance = declinationOpticalAxisDistance;
            }

        /// <summary>
        /// The distance east-west from the dome centre to the mount centre.
        /// A positive value indicates that the mount centre is east of the dome centre.
        /// </summary>
/// <remarks>Referred to as <c>Xm</c> in [Wallace]</remarks>
        public Quantity MountOffsetEast { get; internal set; }
        /// <summary>
        /// The distance north-south from the dome centre to the mount centre.
        /// A positive value indicates that the mount centre is north of the dome centre.
        /// </summary>
/// <remarks>Referred to as <c>Ym</c> in [Wallace].</remarks>
        public Quantity MountOffsetNorth { get; internal set; }
        /// <summary>
        /// The distance up-down from the dome centre to the mount centre.
        /// A positive value indicates that the mount centre is above the dome centre.
        /// </summary>
/// <remarks>Referred to as <c>Zm</c> in [Wallace].</remarks>
        public Quantity MountOffsetUp { get; internal set; }

        /// <summary>
        /// The radius of the dome.
        /// </summary>
        /// <value>
/// SI meters represented as runtime Quantity.
        /// </value>
        /// <remarks>Referred to a <c>Rd</c> in [Wallace].</remarks>
        public Quantity DomeRadius { get; internal set; }

        /// <summary>
        /// The geographic latitude of the observatory, which defines the inclination of the north
        /// end (which points towards Polaris) of the polar axis. Note: in the southern hemisphere
        /// the north end of the polar axis points below the horizon and will be a negative value.
        /// </summary>
        public Latitude ObservatoryLatitude { get; internal set; }

        /// <summary>
        /// Gets the polar-declination axis separation at closest approach.
        /// This is typically zero because for most mount geometries the two axes intersect,
        /// but this is not always so (for example in the case of some horseshoe mounts).
        /// </summary>
        /// <value>
/// SI meters represented as runtime Quantity.
        /// </value>
        public Quantity PolarDeclinationAxisDistance { get; internal set; }

        /// <summary>
        /// The distance along the declination axis from the polar axis to the optical axis.
        /// Typically the declination axis intersects both the polar axis and the optical axis,
        /// so this will be the intersection distance. In cases where the axes do not intersect,
        /// the distance is measured from the point on the declination axis closest to the polar axis,
        /// to the point on the declination axis closest to the optical axis.
        /// </summary>
        /// <value>
/// SI meters represented as runtime Quantity.
        /// </value>
        public Quantity PolarOpticalAxisDistance { get; internal set; }

        /// <summary>
        /// Gets the distance from the declination axis to the optical axis.
        /// This is typically zero for most mounts because the optical assembly
        /// is typically centered on the declination axis for balance. However in some cases,
        /// for example when there are multiple instruments on the same mount, the optical
        /// axis could be mounted off to one side.
        /// </summary>
        /// <value>
/// SI meters represented as runtime Quantity.
        /// </value>
        /// <remarks>This is referred to as <c>r</c> in [Wallace].</remarks>
        public Quantity DeclinationOpticalAxisDistance { get; internal set; }
        }
    }
