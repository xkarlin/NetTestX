using Microsoft.CodeAnalysis;

namespace NetTestX.CodeAnalysis.MethodCollectors;

public readonly struct MethodCollectionContext
{
    public required INamedTypeSymbol Type { get; init; }

    public required Compilation Compilation { get; init; }
}
