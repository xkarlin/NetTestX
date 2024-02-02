using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Generation.MethodCollectors;
using NetTestX.CodeAnalysis.Generation.MockValueProviders;
using NetTestX.CodeAnalysis.Generation.TestFrameworkModels;
using NetTestX.CodeAnalysis.Templates;
using NetTestX.CodeAnalysis.Templates.TestMethods;
using NetTestX.Razor;

namespace NetTestX.CodeAnalysis;

public class UnitTestGeneratorDriver(UnitTestGeneratorContext context)
{
    public async Task<string> GenerateTestClassSourceAsync()
    {
        var model = CreateTestClassModel();
        RazorFileTemplate template = new(model);

        string sourceText = await template.RenderAsync();
        return sourceText;
    }

    private TestClassModel CreateTestClassModel()
    {
        var testMethods = CollectTestMethods();

        var testValueProvider = MockValueProviderLocator.LocateValueProvider(context.Options.MockingLibrary);
        var frameworkModel = TestFrameworkModelLocator.LocateModel(context.Options.TestFramework);

        TestClassModel model = new(
            context.Options.TestClassName,
            context.Options.TestClassNamespace,
            context.Type,
            testValueProvider,
            frameworkModel,
            testMethods);

        return model;
    }

    private IReadOnlyCollection<TestMethodModelBase> CollectTestMethods()
    {
        List<TestMethodModelBase> testMethods = [];

        var collectors = MethodCollectorLocator.GetAvailableCollectors();

        MethodCollectionContext collectionContext = new()
        {
            Type = context.Type,
            Compilation = context.Compilation
        };

        HashSet<ISymbol> excludedSymbols = new(SymbolEqualityComparer.Default);

        foreach (var collector in collectors)
        {
            var collectorExcludedSymbols = collector.GetExcludedSymbols(collectionContext);
            excludedSymbols.UnionWith(collectorExcludedSymbols);
        }

        foreach (var symbol in context.Type.GetMembers().Append(context.Type))
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
}
