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
    public string TestClassName { get; }

    public string TestClassNamespace { get; }

    public INamedTypeSymbol Type { get; }

    public IMockValueProvider ValueProvider { get; }

    public ITestFrameworkModel TestFrameworkModel { get; }

    public IReadOnlyCollection<TestMethodModelBase> TestMethods { get; }

    public TestClassModel(
        string testClassName,
        string testClassNamespace,
        INamedTypeSymbol type,
        IMockValueProvider valueProvider,
        ITestFrameworkModel testFrameworkModel,
        IReadOnlyCollection<TestMethodModelBase> testMethods)
    {
        TestClassName = testClassName;
        TestClassNamespace = testClassNamespace;
        Type = type;
        ValueProvider = valueProvider;
        TestFrameworkModel = testFrameworkModel;
        TestMethods = testMethods;
        
        foreach (var model in testMethods)
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
