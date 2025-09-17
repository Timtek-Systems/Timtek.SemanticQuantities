using System.Globalization;
using System.Linq;
using Timtek.SemanticQuantities.Dimensions;

namespace Timtek.SemanticQuantities.Registry;

public static class UnitExpressionParser
{
    private sealed class Token
    {
        public string Text { get; }
        public int Exponent { get; }
        public bool IsDenominator { get; }
        public Token(string text, int exponent, bool isDenominator)
        {
            Text = text; Exponent = exponent; IsDenominator = isDenominator;
        }
    }

    public static bool TryParse(string input, IDimensionRegistry registry, out Units.IUnit? unit)
    {
        unit = null;
        if (string.IsNullOrWhiteSpace(input)) return false;
        var expr = input.Trim();

        // Quick path: exact unit symbol or name
        try
        {
            var u = registry.ParseUnit(expr);
            unit = u;
            return true;
        }
        catch { /* fall through to expression parse */ }

        // Tokenize into numerator and denominator terms split on '/'
        var slashParts = Split(expr, '/');
        if (slashParts.Count == 0) return false;

        var tokens = new List<Token>();
        for (int i = 0; i < slashParts.Count; i++)
        {
            var part = slashParts[i];
            var isDen = i > 0; // anything after first is denominator
            var factors = Split(part, '*');
            foreach (var f in factors)
            {
                var (name, expOk, expVal) = ParseFactor(f);
                if (!expOk) return false;
                tokens.Add(new Token(name, expVal, isDen));
            }
        }

        // Resolve units, accumulate signature and remember systems
        DimensionSignature sig = DimensionSignature.Dimensionless;
        var systems = new HashSet<UnitSystem>();
        var orderedNumerator = new List<(string sym, int exp)>();
        var orderedDenominator = new List<(string sym, int exp)>();
        foreach (var t in tokens)
        {
            Units.IUnit u;
            try { u = registry.ParseUnit(t.Text); }
            catch { return false; }

            var sys = registry.GetUnitSystem(u);
            systems.Add(sys);

            var e = t.IsDenominator ? -t.Exponent : t.Exponent;
            sig = sig + Scale(u.Signature, e);

            var sym = u.Symbol;
            if (e > 0) orderedNumerator.Add((sym, e));
            else if (e < 0) orderedDenominator.Add((sym, -e));
        }

        var system = systems.Count == 1 ? systems.First() : UnitSystem.SI;
        var symbol = BuildSymbol(orderedNumerator, orderedDenominator);
        unit = new Units.CompoundUnit(symbol, sig, system);
        return true;
    }

    private static (string name, bool ok, int exp) ParseFactor(string f)
    {
        var s = f.Trim();
        if (s.Length == 0) return (string.Empty, false, 0);
        var caret = s.IndexOf('^');
        if (caret < 0)
        {
            return (s, true, 1);
        }
        var name = s.Substring(0, caret).Trim();
        var expStr = s.Substring(caret + 1).Trim();
        if (name.Length == 0 || expStr.Length == 0) return (string.Empty, false, 0);
        if (!int.TryParse(expStr, NumberStyles.Integer, CultureInfo.InvariantCulture, out var n))
            return (string.Empty, false, 0);
        return (name, true, n);
    }

    private static List<string> Split(string s, char sep)
    {
        var parts = s.Split(new[] { sep }, StringSplitOptions.RemoveEmptyEntries)
                     .Select(p => p.Trim())
                     .Where(p => p.Length > 0)
                     .ToList();
        return parts;
    }

    private static DimensionSignature Scale(DimensionSignature s, int k)
        => new(
            s.L * k,
            s.M * k,
            s.T * k,
            s.I * k,
            s.Theta * k,
            s.N * k,
            s.J * k,
            s.Angle * k);

    private static string BuildSymbol(List<(string sym, int exp)> num, List<(string sym, int exp)> den)
    {
        string Join(IEnumerable<(string sym, int exp)> list)
            => string.Join("*", list.Select(x => x.exp == 1 ? x.sym : $"{x.sym}^{x.exp}"));
        var left = num.Count == 0 ? "1" : Join(num);
        if (den.Count == 0) return left;
        return $"{left}/{Join(den)}";
    }
}
