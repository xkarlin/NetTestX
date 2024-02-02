using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Templates.TestMethods;

namespace NetTestX.CodeAnalysis.Generation.MethodCollectors;

public interface ITestMethodCollector
{
    IEnumerable<ISymbol> GetExcludedSymbols(MethodCollectionContext context);

    bool ShouldCollectSymbol(MethodCollectionContext context, ISymbol symbol);

    TestMethodModelBase CollectSymbol(MethodCollectionContext context, ISymbol symbol);
}
