using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Templates.TestMethods;
using NetTestX.CodeAnalysis.Templates.TestMethods.Bodies;

namespace NetTestX.CodeAnalysis.Generation.MethodCollectors;

public class AccessibleStaticMethodCollector : ITestMethodCollector
{
    public bool ShouldCollectSymbol(MethodCollectionContext context, ISymbol symbol)
    {
        if (symbol is not IMethodSymbol method)
            return false;

        if (method.MethodKind != MethodKind.Ordinary)
            return false;

        if (method.DeclaredAccessibility is not Accessibility.Public)
            return false;

        if (!method.IsStatic)
            return false;

        return true;
    }

    public TestMethodModelBase CollectSymbol(MethodCollectionContext context, ISymbol symbol)
    {
        var method = (IMethodSymbol)symbol;

        var bodyModel = new AccessibleStaticMethodBodyModel(method);

        if (method.IsAsync)
            return new AsyncTestMethodModel(symbol, bodyModel);

        return new TestMethodModel(symbol, bodyModel);
    }
}
