using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Extensions;
using NetTestX.CodeAnalysis.Generation.ConstructorResolvers;
using NetTestX.CodeAnalysis.Templates.TestMethods;
using NetTestX.CodeAnalysis.Templates.TestMethods.Bodies;

namespace NetTestX.CodeAnalysis.Generation.MethodCollectors;

public class AccessibleInstanceMethodCollector : ITestMethodCollector
{
    public IEnumerable<ISymbol> GetExcludedSymbols(MethodCollectionContext context) => [];

    public bool ShouldCollectSymbol(MethodCollectionContext context, ISymbol symbol)
    {
        if (symbol is not IMethodSymbol method)
            return false;

        if (!symbol.ContainingType.HasAccessibleConstructor())
            return false;

        if (method.MethodKind is not MethodKind.Ordinary)
            return false;

        if (method.DeclaredAccessibility is not Accessibility.Public)
            return false;

        if (method.IsStatic || method.IsAbstract)
            return false;

        return true;
    }

    public TestMethodModelBase CollectSymbol(MethodCollectionContext context, ISymbol symbol)
    {
        var method = (IMethodSymbol)symbol;
        method = SymbolGenerationResolver.Resolve(method, context.Compilation);

        var constructorResolver = new DummyConstructorResolver();
        var constructor = constructorResolver.Resolve(context.Type);

        var bodyModel = new AccessibleInstanceMethodBodyModel(method, constructor);

        if (method.IsAsync)
            return new AsyncTestMethodModel(symbol, bodyModel);

        return new TestMethodModel(symbol, bodyModel);
    }
}
