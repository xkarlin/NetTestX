using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Templates.TestMethods;

namespace NetTestX.CodeAnalysis.Generation.MethodCollectors;

/// <summary>
/// Represents a collector that finds test methods for <see cref="INamedTypeSymbol"/>s
/// </summary>
public interface ITestMethodCollector
{
    /// <summary>
    /// Get all symbols that should be invisible to other <see cref="ITestMethodCollector"/>s
    /// </summary>
    IEnumerable<ISymbol> GetExcludedSymbols(MethodCollectionContext context);

    /// <summary>
    /// Whether the given <paramref name="symbol"/> should be collected.
    /// If <c>true</c> is returned, <see cref="CollectSymbol"/> will be called on that symbol later
    /// </summary>
    bool ShouldCollectSymbol(MethodCollectionContext context, ISymbol symbol);

    /// <summary>
    /// Materialize the given <paramref name="symbol"/> into an instance of <see cref="TestMethodModelBase"/>
    /// </summary>
    TestMethodModelBase CollectSymbol(MethodCollectionContext context, ISymbol symbol);
}
