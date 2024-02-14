using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Extensions;
using NetTestX.CodeAnalysis.Generation.ConstructorResolvers;
using NetTestX.CodeAnalysis.Templates.TestMethods;
using NetTestX.CodeAnalysis.Templates.TestMethods.Bodies;
using System;
using System.Collections.Generic;

namespace NetTestX.CodeAnalysis.Generation.MethodCollectors;

public class AsyncDisposableTypeCollector : ITestMethodCollector
{
    public IEnumerable<ISymbol> GetExcludedSymbols(MethodCollectionContext context) =>
    [
        .. context.Type.FindImplementationsForInterfaceMembers<IAsyncDisposable>(context.Compilation)
    ];

    public bool ShouldCollectSymbol(MethodCollectionContext context, ISymbol symbol)
    {
        if (symbol is not INamedTypeSymbol namedType)
            return false;

        if (namedType.DeclaredAccessibility is not Accessibility.Public)
            return false;

        return namedType.ImplementsInterface<IAsyncDisposable>();
    }

    public TestMethodModelBase CollectSymbol(MethodCollectionContext context, ISymbol symbol)
    {
        var constructorResolver = new DummyConstructorResolver();
        var constructor = constructorResolver.Resolve(context.Type);

        DisposableTypeMethodBodyModel model = new(constructor, true);
        return new AsyncTestMethodModel(symbol, model, "TestAsyncDisposable");
    }
}
