using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Templates.TestMethods;

namespace NetTestX.CodeAnalysis;

/// <summary>
/// Context passed to <see cref="UnitTestGeneratorDriver"/> containing all information
/// necessary to generate the tests
/// </summary>
public class UnitTestGeneratorContext
{
    /// <summary>
    /// The <see cref="INamedTypeSymbol"/> for which the tests are generated
    /// </summary>
    public required INamedTypeSymbol Type { get; init; }

    /// <summary>
    /// The <see cref="Microsoft.CodeAnalysis.Compilation"/> that contains the <see cref="Type"/>
    /// </summary>
    public required Compilation Compilation { get; init; }

    /// <summary>
    /// A list of all <see cref="TestMethodModelBase"/>s that will be generated as test methods
    /// </summary>
    public required IReadOnlyCollection<TestMethodModelBase> TestMethods { get; init; }

    /// <summary>
    /// Options that the generator should use
    /// </summary>
    public required UnitTestGeneratorOptions Options { get; init; }
}
