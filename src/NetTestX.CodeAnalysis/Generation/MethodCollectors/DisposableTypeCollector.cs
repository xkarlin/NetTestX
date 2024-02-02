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
        ..context.Type.FindImplementationsForInterfaceMembers<IDisposable>(context.Compilation),
        ..context.Type.FindImplementationsForInterfaceMembers<IAsyncDisposable>(context.Compilation)
    ];
    
    public bool ShouldCollectSymbol(MethodCollectionContext context, ISymbol symbol)
    {
        if (symbol is not INamedTypeSymbol namedType)
            return false;

        if (namedType.DeclaredAccessibility is not Accessibility.Public)
            return false;

        if (namedType.ImplementsInterface<IDisposable>() || namedType.ImplementsInterface<IAsyncDisposable>())
            return true;

        return false;
    }

    public TestMethodModelBase CollectSymbol(MethodCollectionContext context, ISymbol symbol)
    {
        var namedType = (INamedTypeSymbol)symbol;

        var constructorResolver = new DummyConstructorResolver();
        var constructor = constructorResolver.Resolve(context.Type);

        DisposableTypeMethodBodyModel model = new(constructor, !namedType.ImplementsInterface<IDisposable>());

        if (model.IsAsync)
            return new AsyncTestMethodModel(symbol, model, "TestAsyncDisposable");

        return new TestMethodModel(symbol, model, "TestDisposable");
    }
}
