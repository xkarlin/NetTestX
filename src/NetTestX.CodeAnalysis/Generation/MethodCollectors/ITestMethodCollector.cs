using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Templates.TestMethods;

namespace NetTestX.CodeAnalysis.Generation.MethodCollectors;

public interface ITestMethodCollector
{
    bool ShouldCollectSymbol(MethodCollectionContext context, ISymbol symbol);

    ITestMethodModel CollectSymbol(MethodCollectionContext context, ISymbol symbol);
}
