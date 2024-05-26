using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Templates.TestMethods;
using System.Collections.Generic;
using System.Linq;
using NetTestX.CodeAnalysis.Extensions;
using NetTestX.Common.Diagnostics;
using NetTestX.Common.Extensions;

namespace NetTestX.CodeAnalysis.Generation.MethodCollectors;

/// <summary>
/// Helper class for <see cref="ITestMethodCollector"/>s
/// </summary>
public static class MethodCollectorHelper
{
    /// <summary>
    /// Retrieve all available test method collectors
    /// </summary>
    public static IEnumerable<ITestMethodCollector> GetAvailableCollectors() =>
    [
        new AccessibleInstanceMethodCollector(),
        new AccessibleStaticMethodCollector(),
        new AccessibleIndexerCollector(),
        new DisposableTypeCollector(),
        new AsyncDisposableTypeCollector()
    ];

    /// <summary>
    /// Collect a list of <see cref="TestMethodModelBase"/> for the given <paramref name="type"/>
    /// using all available <see cref="ITestMethodCollector"/>s
    /// </summary>
    public static IReadOnlyList<TestMethodModelBase> CollectTestMethods(
        INamedTypeSymbol type,
        Compilation compilation,
        AdvancedGeneratorOptions advancedOptions,
        IDiagnosticReporter reporter = null)
    {
        CheckTypeDiagnostics(type, advancedOptions, reporter);

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

    private static void CheckTypeDiagnostics(INamedTypeSymbol type, AdvancedGeneratorOptions advancedOptions, IDiagnosticReporter reporter)
    {
        if (reporter is null)
            return;

        var accessibility = (advancedOptions & AdvancedGeneratorOptions.IncludeInternalMembers) != 0 ? Accessibility.Internal : Accessibility.Public;

        if (!type.HasAccessibleConstructor(accessibility))
            reporter.ReportWarning($"The type {type.Name} does not expose any accessible instance constructor. Some members are not available for generation.");
    }
}
