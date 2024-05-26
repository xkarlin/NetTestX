using NetTestX.Common;

namespace NetTestX.CodeAnalysis;

/// <summary>
/// Options containing specific generation data used by <see cref="UnitTestGeneratorDriver"/>
/// </summary>
public class UnitTestGeneratorOptions
{
    /// <summary>
    /// The name of the generated test class
    /// </summary>
    public required string TestClassName { get; init; }

    /// <summary>
    /// The name of the generated class namespace
    /// </summary>
    public required string TestClassNamespace { get; init; }

    /// <summary>
    /// Test framework used by the generated tests
    /// </summary>
    public required TestFramework TestFramework { get; init; }

    /// <summary>
    /// Mocking library used by the generated tests
    /// </summary>
    public required MockingLibrary MockingLibrary { get; init; }

    /// <summary>
    /// Any advanced generator options that the generated tests should respect
    /// </summary>
    public AdvancedGeneratorOptions AdvancedOptions { get; init; }
}
