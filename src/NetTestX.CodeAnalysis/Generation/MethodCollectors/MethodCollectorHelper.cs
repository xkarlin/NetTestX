using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Templates.TestMethods;
using System.Collections.Generic;
using System.Linq;
using NetTestX.CodeAnalysis.Extensions;
using NetTestX.Common.Diagnostics;
using NetTestX.Common.Extensions;

namespace NetTestX.CodeAnalysis.Generation.MethodCollectors;

public static class MethodCollectorHelper
{
    public static IEnumerable<ITestMethodCollector> GetAvailableCollectors() =>
    [
        new AccessibleInstanceMethodCollector(),
        new AccessibleStaticMethodCollector(),
        new AccessibleIndexerCollector(),
        new DisposableTypeCollector(),
        new AsyncDisposableTypeCollector()
    ];

    public static IReadOnlyList<TestMethodModelBase> CollectTestMethods(
        INamedTypeSymbol type,
        Compilation compilation,
        AdvancedGeneratorOptions advancedOptions,
        IDiagnosticReporter reporter = null)
    {
        CheckTypeDiagnostics(type, reporter);

        List<TestMethodModelBase> testMethods = [];

        var collectors = GetAvailableCollectors();

        MethodCollectionContext collectionContext = new()
        {
            Type = type,
            Compilation = compilation,
            AdvancedOptions = advancedOptions
        };

        HashSet<ISymbol> excludedSymbols = new(SymbolEqualityComparer.Default);

        foreach (var collector in collectors)
        {
            var collectorExcludedSymbols = collector.GetExcludedSymbols(collectionContext);
            excludedSymbols.UnionWith(collectorExcludedSymbols);
        }

        foreach (var symbol in type.GetMembers().Append(type))
        {
            if (excludedSymbols.Contains(symbol) || !ShouldCollectSymbol(symbol))
                continue;

            foreach (var collector in collectors)
            {
                if (collector.ShouldCollectSymbol(collectionContext, symbol))
                {
                    var testMethod = collector.CollectSymbol(collectionContext, symbol);
                    testMethods.Add(testMethod);
                }
            }
        }

        return testMethods;

        static bool ShouldCollectSymbol(ISymbol symbol)
        {
            if (symbol.IsImplicitlyDeclared)
                return false;

            if (symbol is ITypeSymbol { ContainingType: not null })
                return false;

            return true;
        }
    }

    private static void CheckTypeDiagnostics(INamedTypeSymbol type, IDiagnosticReporter reporter)
    {
        if (reporter is null)
            return;

        if (!type.HasAccessibleConstructor())
            reporter.ReportWarning($"The type {type.Name} does not expose any accessible instance constructor. Some members are not available for generation.");
    }
}
