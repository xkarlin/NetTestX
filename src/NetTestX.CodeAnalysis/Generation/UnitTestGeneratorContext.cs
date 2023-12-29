using Microsoft.CodeAnalysis;

namespace NetTestX.CodeAnalysis.Generation;

public class UnitTestGeneratorContext
{
    public required Compilation Compilation { get; init; }

    public required INamedTypeSymbol Type { get; init; }

    public required UnitTestGeneratorOptions Options { get; init; }
}
