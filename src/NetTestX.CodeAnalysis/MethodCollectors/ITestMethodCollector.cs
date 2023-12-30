using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Templates;

namespace NetTestX.CodeAnalysis.MethodCollectors;

public interface ITestMethodCollector
{
    bool ShouldCollectSymbol(MethodCollectionContext context, ISymbol symbol);

    TestMethodModel CollectSymbol(MethodCollectionContext context, ISymbol symbol);
}
