using Microsoft.CodeAnalysis;

namespace NetTestX.CodeAnalysis.Generation.MethodCollectors;

public readonly struct MethodCollectionContext
{
    public required INamedTypeSymbol Type { get; init; }

    public required Compilation Compilation { get; init; }

    public AdvancedGeneratorOptions AdvancedOptions { get; init; }

    public Accessibility EffectiveVisibility => (AdvancedOptions & AdvancedGeneratorOptions.IncludeInternalMembers) != 0 ? Accessibility.Internal : Accessibility.Public;
}
