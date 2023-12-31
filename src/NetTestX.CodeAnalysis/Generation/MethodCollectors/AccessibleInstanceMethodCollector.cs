using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Templates;
using NetTestX.CodeAnalysis.Templates.TestMethods;
using NetTestX.CodeAnalysis.Templates.TestMethods.Bodies;

namespace NetTestX.CodeAnalysis.Generation.MethodCollectors;

public class AccessibleInstanceMethodCollector : ITestMethodCollector
{
    public bool ShouldCollectSymbol(MethodCollectionContext context, ISymbol symbol)
    {
        if (symbol is not IMethodSymbol method)
            return false;

        if (method.MethodKind != MethodKind.Ordinary)
            return false;

        if (method.DeclaredAccessibility is not Accessibility.Public)
            return false;

        if (method.IsStatic || method.IsAbstract)
            return false;

        return true;
    }

    public ITestMethodModel CollectSymbol(MethodCollectionContext context, ISymbol symbol)
    {
        var method = (IMethodSymbol)symbol;

        var bodyModel = new AccessibleInstanceMethodBodyModel(method);

        if (method.IsAsync)
            return new AsyncTestMethodModel(symbol, bodyModel);

        return new TestMethodModel(symbol, bodyModel);
    }
}
