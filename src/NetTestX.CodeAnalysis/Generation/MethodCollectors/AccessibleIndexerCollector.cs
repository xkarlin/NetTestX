using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Common;
using NetTestX.CodeAnalysis.Generation.ConstructorResolvers;
using NetTestX.CodeAnalysis.Templates.TestMethods;
using NetTestX.CodeAnalysis.Templates.TestMethods.Bodies;

namespace NetTestX.CodeAnalysis.Generation.MethodCollectors;

public class AccessibleIndexerCollector : ITestMethodCollector
{
    public IEnumerable<ISymbol> GetExcludedSymbols(MethodCollectionContext context) => [];

    public bool ShouldCollectSymbol(MethodCollectionContext context, ISymbol symbol)
    {
        if (symbol is not IPropertySymbol { IsIndexer: true } indexer)
            return false;

        if (indexer.DeclaredAccessibility is not Accessibility.Public)
            return false;

        if (indexer.IsAbstract)
            return false;

        return true;
    }

    public TestMethodModelBase CollectSymbol(MethodCollectionContext context, ISymbol symbol)
    {
        var indexer = (IPropertySymbol)symbol;

        var constructorResolver = new DummyConstructorResolver();
        var constructor = constructorResolver.Resolve(context.Type);

        var bodyModel = new AccessibleIndexerMethodBodyModel(indexer, constructor);

        string methodName = $"Test{string.Join("", indexer.Parameters.Select(x => x.Type.ToDisplayString(CommonFormats.NameOnlyFormat)))}Indexer";

        return new TestMethodModel(symbol, bodyModel, methodName);
    }
}
