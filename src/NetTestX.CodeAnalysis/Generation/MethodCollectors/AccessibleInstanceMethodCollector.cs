using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Extensions;
using NetTestX.CodeAnalysis.Generation.ConstructorResolvers;
using NetTestX.CodeAnalysis.Templates.TestMethods;
using NetTestX.CodeAnalysis.Templates.TestMethods.Bodies;

namespace NetTestX.CodeAnalysis.Generation.MethodCollectors;

/// <summary>
/// Test method collector for accessible instance methods
/// </summary>
public class AccessibleInstanceMethodCollector : ITestMethodCollector
{
    public IEnumerable<ISymbol> GetExcludedSymbols(MethodCollectionContext context) => [];

    public bool ShouldCollectSymbol(MethodCollectionContext context, ISymbol symbol)
    {
        if (symbol is not IMethodSymbol method)
            return false;

        if (!symbol.ContainingType.HasAccessibleConstructor(context.EffectiveVisibility))
            return false;

        if (method.MethodKind is not MethodKind.Ordinary)
            return false;

        if (method.GetEffectiveAccessibility() < context.EffectiveVisibility)
            return false;

        if (method.IsStatic || method.IsAbstract)
            return false;

        return true;
    }

    public TestMethodModelBase CollectSymbol(MethodCollectionContext context, ISymbol symbol)
    {
        var method = (IMethodSymbol)symbol;
        method = SymbolGenerationResolver.Resolve(method, context.Compilation, context.AdvancedOptions);

        var constructorResolver = new DummyConstructorResolver(context.EffectiveVisibility);
        var constructor = constructorResolver.Resolve(context.Type);

        var bodyModel = new AccessibleInstanceMethodBodyModel(method, constructor);

        if (method.IsAsync)
            return new AsyncTestMethodModel(symbol, bodyModel);

        return new TestMethodModel(symbol, bodyModel);
    }
}
