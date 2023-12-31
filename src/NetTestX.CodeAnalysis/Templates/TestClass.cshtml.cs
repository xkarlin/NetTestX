using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Generation.ConstructorResolvers;
using NetTestX.CodeAnalysis.Generation.TypeValueProviders;
using NetTestX.CodeAnalysis.Templates.TestMethods;

namespace NetTestX.CodeAnalysis.Templates;

public class TestClassModel
{
    public string TestClassName { get; }

    public string TestClassNamespace { get; }

    public INamedTypeSymbol Type { get; }

    public IConstructorResolver ConstructorResolver { get; }

    public ITypeValueProvider ValueProvider { get; }

    public IReadOnlyCollection<ITestMethodModel> TestMethods { get; }

    public IMethodSymbol Constructor { get; }

    public TestClassModel(
        string testClassName,
        string testClassNamespace,
        INamedTypeSymbol type,
        IConstructorResolver constructorResolver,
        ITypeValueProvider valueProvider,
        IReadOnlyCollection<ITestMethodModel> testMethods)
    {
        TestClassName = testClassName;
        TestClassNamespace = testClassNamespace;
        Type = type;
        ConstructorResolver = constructorResolver;
        ValueProvider = valueProvider;
        TestMethods = testMethods;

        Constructor = ConstructorResolver.Resolve(type);
        
        foreach (var model in testMethods)
            model.Parent = this;
    }

    public string Value(ITypeSymbol type) => ValueProvider.Resolve(type);
}
