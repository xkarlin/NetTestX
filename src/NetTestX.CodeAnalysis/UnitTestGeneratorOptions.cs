using NetTestX.CodeAnalysis.Templates.TestMethods;
using NetTestX.Common;
using System.Collections.Generic;

namespace NetTestX.CodeAnalysis;

public class UnitTestGeneratorOptions
{
    public required string TestClassName { get; init; }

    public required string TestClassNamespace { get; init; }

    public required TestFramework TestFramework { get; init; }

    public required MockingLibrary MockingLibrary { get; init; }

    public required IReadOnlyCollection<TestMethodModelBase> TestMethods { get; init; }
}
