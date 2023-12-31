using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Generation.ConstructorResolvers;
using NetTestX.CodeAnalysis.Generation.MethodCollectors;
using NetTestX.CodeAnalysis.Generation.TypeValueProviders;
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

        TestClassModel model = new(
            context.Options.TestClassName,
            context.Options.TestClassNamespace,
            context.Type,
            new DummyConstructorResolver(),
            new NSubstituteValueProvider(),
            testMethods);

        return model;
    }

    private IReadOnlyCollection<TestMethodModelBase> CollectTestMethods()
    {
        List<TestMethodModelBase> testMethods = [];

        var collectors = MethodCollectorLocator.GetAvailableCollectors();

        MethodCollectionContext collectionContext = new()
        {
            Compilation = context.Compilation,
            Type = context.Type
        };

        foreach (var symbol in context.Type.GetMembers())
        {
            if (!ShouldCollectSymbol(symbol))
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

            if (symbol is ITypeSymbol)
                return false;

            return true;
        }
    }
}
