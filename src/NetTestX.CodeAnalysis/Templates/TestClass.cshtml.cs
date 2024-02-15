using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Extensions;
using NetTestX.CodeAnalysis.Generation;
using NetTestX.CodeAnalysis.Generation.MockValueProviders;
using NetTestX.CodeAnalysis.Generation.TestFrameworkModels;
using NetTestX.CodeAnalysis.Templates.TestMethods;

namespace NetTestX.CodeAnalysis.Templates;

public class TestClassModel : INamespaceCollector
{
    public required string TestClassName { get; init; }

    public required string TestClassNamespace { get; init; }

    public required INamedTypeSymbol Type { get; init; }

    public required IMockValueProvider ValueProvider { get; init; }

    public required ITestFrameworkModel TestFrameworkModel { get; init; }

    public AdvancedGeneratorOptions AdvancedOptions { get; init; } = AdvancedGeneratorOptions.Default;

    public IReadOnlyCollection<TestMethodModelBase> TestMethods { get; }

    public TestClassModel(IReadOnlyCollection<TestMethodModelBase> testMethods)
    {
        TestMethods = testMethods;

        foreach (var model in TestMethods)
            model.Parent = this;
    }

    public string Value(ITypeSymbol type) => ValueProvider.Resolve(type);

    public IEnumerable<string> CollectNamespaces()
    {
        IEnumerable<string> namespaces = NamespaceHelper.DefaultNamespaces;

        namespaces = namespaces.Union(Type.CollectNamespaces());

        namespaces = namespaces.Union(ValueProvider.CollectNamespaces());

        namespaces = namespaces.Union(TestFrameworkModel.CollectNamespaces());

        foreach (var model in TestMethods)
            namespaces = namespaces.Union(model.CollectNamespaces());

        return namespaces;
    }
}
