namespace Timtek.SemanticQuantities.Dimensions;

/// <summary>
/// Immutable dimensional signature with SI base dimensions plus Angle as a distinct base dimension.
/// The signature consists of integer exponents in the order:
/// L (length), M (mass), T (time), I (electric current), Θ (temperature), N (amount), J (luminous intensity), A (angle).
/// </summary>
public readonly struct DimensionSignature : IEquatable<DimensionSignature>
{
    public int L { get; }
    public int M { get; }
    public int T { get; }
    public int I { get; }
    public int Theta { get; }
    public int N { get; }
    public int J { get; }
    public int Angle { get; }

    public DimensionSignature(int l, int m, int t, int i, int theta, int n, int j, int angle)
    {
        L = l; M = m; T = t; I = i; Theta = theta; N = n; J = j; Angle = angle;
    }

    public static readonly DimensionSignature Dimensionless = new(0, 0, 0, 0, 0, 0, 0, 0);
    public static readonly DimensionSignature Length        = new(1, 0, 0, 0, 0, 0, 0, 0);
    public static readonly DimensionSignature Mass          = new(0, 1, 0, 0, 0, 0, 0, 0);
    public static readonly DimensionSignature Time          = new(0, 0, 1, 0, 0, 0, 0, 0);
    public static readonly DimensionSignature ElectricCurrent = new(0, 0, 0, 1, 0, 0, 0, 0);
    public static readonly DimensionSignature Temperature   = new(0, 0, 0, 0, 1, 0, 0, 0);
    public static readonly DimensionSignature Amount        = new(0, 0, 0, 0, 0, 1, 0, 0);
    public static readonly DimensionSignature LuminousIntensity = new(0, 0, 0, 0, 0, 0, 1, 0);
public static readonly DimensionSignature Angular       = new(0, 0, 0, 0, 0, 0, 0, 1);

    public static DimensionSignature operator +(DimensionSignature a, DimensionSignature b)
        => new(a.L + b.L, a.M + b.M, a.T + b.T, a.I + b.I, a.Theta + b.Theta, a.N + b.N, a.J + b.J, a.Angle + b.Angle);

    public static DimensionSignature operator -(DimensionSignature a, DimensionSignature b)
        => new(a.L - b.L, a.M - b.M, a.T - b.T, a.I - b.I, a.Theta - b.Theta, a.N - b.N, a.J - b.J, a.Angle - b.Angle);

    public bool Equals(DimensionSignature other)
        => L == other.L && M == other.M && T == other.T && I == other.I && Theta == other.Theta && N == other.N && J == other.J && Angle == other.Angle;

    public override bool Equals(object? obj) => obj is DimensionSignature other && Equals(other);

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = L;
            hash = (hash * 397) ^ M;
            hash = (hash * 397) ^ T;
            hash = (hash * 397) ^ I;
            hash = (hash * 397) ^ Theta;
            hash = (hash * 397) ^ N;
            hash = (hash * 397) ^ J;
            hash = (hash * 397) ^ Angle;
            return hash;
        }
    }

    public override string ToString() => $"[{L},{M},{T},{I},{Theta},{N},{J},{Angle}]";
}
