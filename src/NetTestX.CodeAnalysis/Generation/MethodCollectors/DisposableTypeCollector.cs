using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Extensions;
using NetTestX.CodeAnalysis.Generation.ConstructorResolvers;
using NetTestX.CodeAnalysis.Templates.TestMethods;
using NetTestX.CodeAnalysis.Templates.TestMethods.Bodies;

namespace NetTestX.CodeAnalysis.Generation.MethodCollectors;

public class DisposableTypeCollector : ITestMethodCollector
{
    public IEnumerable<ISymbol> GetExcludedSymbols(MethodCollectionContext context) =>
    [
        ..context.Type.FindImplementationsForInterfaceMembers<IDisposable>(context.Compilation)
    ];
    
    public bool ShouldCollectSymbol(MethodCollectionContext context, ISymbol symbol)
    {
        if (symbol is not INamedTypeSymbol namedType)
            return false;

        if (!namedType.HasAccessibleConstructor(context.EffectiveVisibility))
            return false;

        if (namedType.GetEffectiveAccessibility() < context.EffectiveVisibility)
            return false;

        return namedType.ImplementsInterface<IDisposable>();
    }

    public TestMethodModelBase CollectSymbol(MethodCollectionContext context, ISymbol symbol)
    {
        var constructorResolver = new DummyConstructorResolver();
        var constructor = constructorResolver.Resolve(context.Type);

        DisposableTypeMethodBodyModel model = new(constructor, false);
        return new TestMethodModel(symbol, model, "TestDisposable");
    }
}
