using System.Collections.Concurrent;
using System.Globalization;
using System.Text;
using Timtek.SemanticQuantities.Dimensions;
using Timtek.SemanticQuantities.Units;
using Timtek.SemanticQuantities.Quantities;

namespace Timtek.SemanticQuantities.Registry;

public sealed class DefaultDimensionRegistry : IDimensionRegistry
{
    private readonly Dictionary<string, IUnit> _bySymbol = new(StringComparer.Ordinal);
    private readonly Dictionary<string, IUnit> _byName   = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<DimensionSignature, IUnit> _coherentBySignature = new();

    public void Register(IUnit unit)
    {
        _bySymbol[unit.Symbol] = unit;
        _byName[unit.Name]     = unit;

        // If the unit is coherent SI (a=1, b=0 implied by unit conversion identity),
        // register as preferred for its signature if not yet set or choose SI named unit over compound.
        // For now, assume any unit whose ToSI/FromSI are identity at runtime for coherent SI (coarse heuristic).
        // We allow multiple registers; first wins unless later is an SI named unit replacing a compound.
        if (!_coherentBySignature.ContainsKey(unit.Signature) || _coherentBySignature[unit.Signature] is CompoundUnit)
            _coherentBySignature[unit.Signature] = unit;
    }

    public IUnit ParseUnit(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) throw new FormatException("Unit text is empty");
        if (_bySymbol.TryGetValue(text.Trim(), out var uBySymbol)) return uBySymbol;
        if (_byName.TryGetValue(text.Trim(), out var uByName)) return uByName;
        throw new KeyNotFoundException($"Unknown unit: '{text}'");
    }

    public bool IsKnownUnit(string text)
        => _bySymbol.ContainsKey(text) || _byName.ContainsKey(text);

    public IUnit GetCoherentUnit(DimensionSignature signature)
    {
        if (_coherentBySignature.TryGetValue(signature, out var unit)) return unit;
        // Build a canonical compound symbol from the signature in SI base order: m, kg, s, A, K, mol, cd, rad
        var symbol = BuildCanonicalSymbol(signature);
        var compound = new CompoundUnit(symbol, signature);
        _coherentBySignature[signature] = compound;
        return compound;
    }

    public Quantity Convert(Quantity source, IUnit target)
    {
        if (!source.Signature.Equals(target.Signature))
            throw new InvalidOperationException($"Dimension mismatch: {source.Signature} vs {target.Signature}");
        var value = target.FromSI(source.ValueSI);
        return new Quantity(value, target);
    }

    private static string BuildCanonicalSymbol(DimensionSignature s)
    {
        var partsPos = new List<string>();
        var partsNeg = new List<string>();
        void Add(string sym, int exp)
        {
            var tgt = exp > 0 ? partsPos : partsNeg;
            var e = Math.Abs(exp);
            if (e == 0) return;
            tgt.Add(e == 1 ? sym : $"{sym}^{e}");
        }

        Add("m", s.L);
        Add("kg", s.M);
        Add("s", s.T);
        Add("A", s.I);
        Add("K", s.Theta);
        Add("mol", s.N);
        Add("cd", s.J);
        Add("rad", s.Angle);

        if (partsNeg.Count == 0) return string.Join("*", partsPos);
        var left  = partsPos.Count == 0 ? "1" : string.Join("*", partsPos);
        var right = string.Join("*", partsNeg);
        return $"{left}/{right}";
    }
}
