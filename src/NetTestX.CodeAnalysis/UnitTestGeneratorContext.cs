using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Templates.TestMethods;

namespace NetTestX.CodeAnalysis;

public class UnitTestGeneratorContext
{
    public required INamedTypeSymbol Type { get; init; }

    public required Compilation Compilation { get; init; }

    public required IReadOnlyCollection<TestMethodModelBase> TestMethods { get; init; }

    public required UnitTestGeneratorOptions Options { get; init; }
}
