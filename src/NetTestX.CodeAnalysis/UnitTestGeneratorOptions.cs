using NetTestX.Common;

namespace NetTestX.CodeAnalysis;

public record UnitTestGeneratorOptions
{
    public required string TestClassName { get; init; }

    public required string TestClassNamespace { get; init; }

    public required TestFramework TestFramework { get; init; }

    public required MockingLibrary MockingLibrary { get; init; }
}
