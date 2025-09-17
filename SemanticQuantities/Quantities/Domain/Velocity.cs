using Timtek.SemanticQuantities.Units;

namespace Timtek.SemanticQuantities.Quantities.Domain;

public readonly struct Velocity
{
    public Quantity<MeterPerSecond> Speed     { get; }
    public Direction                Direction { get; }

    private Velocity(Quantity<MeterPerSecond> speed, Direction direction)
    {
        Speed = speed;
        Direction = direction;
    }

    public static Velocity From(Quantity<MeterPerSecond> speed, Direction direction) => new(speed, direction);

    public double Vx => Speed.ValueSI * Direction.X;
    public double Vy => Speed.ValueSI * Direction.Y;
    public double Vz => Speed.ValueSI * Direction.Z;
}