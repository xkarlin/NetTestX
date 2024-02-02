using Microsoft.CodeAnalysis;

namespace NetTestX.CodeAnalysis;

public class UnitTestGeneratorContext
{
    public required INamedTypeSymbol Type { get; init; }

    public required Compilation Compilation { get; init; }

    public required UnitTestGeneratorOptions Options { get; init; }
}
