using Timtek.SemanticQuantities.Registry;

namespace Timtek.SemanticQuantities.Quantities;

/// <summary>
/// Global access to the active dimension registry. Library consumers can set this in their composition root.
/// Defaults to a new DefaultDimensionRegistry if not set.
/// </summary>
public static class DimensionSystem
{
    private static IDimensionRegistry? _registry;

    public static IDimensionRegistry Registry
    {
        get => _registry ??= Registry.RegistryBootstrapper.BuildDefault();
        set => _registry = value ?? throw new ArgumentNullException(nameof(value));
    }
}
