using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using NetTestX.CodeAnalysis.MethodCollectors;
using NetTestX.CodeAnalysis.Templates;
using NetTestX.Razor;

namespace NetTestX.CodeAnalysis.Generation;

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

        TestClassModel model = new()
        {
            TestClassName = context.Options.TestClassName,
            TestClassNamespace = context.Options.TestClassNamespace,
            TestMethods = testMethods
        };

        return model;
    }

    private IEnumerable<TestMethodModel> CollectTestMethods()
    {
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
                    yield return testMethod;
                }
            }
        }

        bool ShouldCollectSymbol(ISymbol symbol)
        {
            if (symbol.IsImplicitlyDeclared)
                return false;

            if (symbol is ITypeSymbol)
                return false;

            return true;
        }
    }
}
