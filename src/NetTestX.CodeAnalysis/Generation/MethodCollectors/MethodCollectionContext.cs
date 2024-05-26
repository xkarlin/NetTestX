using Microsoft.CodeAnalysis;

namespace NetTestX.CodeAnalysis.Generation.MethodCollectors;

/// <summary>
/// Context passed to <see cref="ITestMethodCollector"/>s
/// </summary>
public readonly struct MethodCollectionContext
{
    /// <summary>
    /// The <see cref="INamedTypeSymbol"/> for which test methods are collected
    /// </summary>
    public required INamedTypeSymbol Type { get; init; }

    /// <summary>
    /// The <see cref="Microsoft.CodeAnalysis.Compilation"/> that contains the <see cref="Type"/>
    /// </summary>
    public required Compilation Compilation { get; init; }

    /// <summary>
    /// <see cref="AdvancedGeneratorOptions"/> passed to the context
    /// </summary>
    public AdvancedGeneratorOptions AdvancedOptions { get; init; }

    /// <summary>
    /// Effective visibility of the <see cref="Type"/>
    /// </summary>
    public Accessibility EffectiveVisibility => (AdvancedOptions & AdvancedGeneratorOptions.IncludeInternalMembers) != 0 ? Accessibility.Internal : Accessibility.Public;
}
