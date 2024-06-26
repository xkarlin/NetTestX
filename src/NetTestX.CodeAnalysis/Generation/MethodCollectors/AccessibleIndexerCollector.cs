﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Common;
using NetTestX.CodeAnalysis.Extensions;
using NetTestX.CodeAnalysis.Generation.ConstructorResolvers;
using NetTestX.CodeAnalysis.Templates.TestMethods;
using NetTestX.CodeAnalysis.Templates.TestMethods.Bodies;

namespace NetTestX.CodeAnalysis.Generation.MethodCollectors;

/// <summary>
/// Test method collector for accessible indexer methods
/// </summary>
public class AccessibleIndexerCollector : ITestMethodCollector
{
    public IEnumerable<ISymbol> GetExcludedSymbols(MethodCollectionContext context) => [];

    public bool ShouldCollectSymbol(MethodCollectionContext context, ISymbol symbol)
    {
        if (symbol is not IPropertySymbol { IsIndexer: true } indexer)
            return false;

        if (!symbol.ContainingType.HasAccessibleConstructor(context.EffectiveVisibility))
            return false;

        if (indexer.GetEffectiveAccessibility() < context.EffectiveVisibility)
            return false;

        if (indexer.IsAbstract)
            return false;

        return true;
    }

    public TestMethodModelBase CollectSymbol(MethodCollectionContext context, ISymbol symbol)
    {
        var indexer = (IPropertySymbol)symbol;

        var constructorResolver = new DummyConstructorResolver(context.EffectiveVisibility);
        var constructor = constructorResolver.Resolve(context.Type);

        var bodyModel = new AccessibleIndexerMethodBodyModel(indexer, constructor);

        string methodName = $"Test{string.Join("", indexer.Parameters.Select(x => x.Type.ToDisplayString(CommonFormats.NameOnlyFormatNoSpecialTypes)))}Indexer";

        return new TestMethodModel(symbol, bodyModel, methodName);
    }
}
