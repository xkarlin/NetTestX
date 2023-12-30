using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Templates;

namespace NetTestX.CodeAnalysis.MethodCollectors;

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

    public TestMethodModel CollectSymbol(MethodCollectionContext context, ISymbol symbol)
    {
        return new TestMethodModel
        {
            Symbol = symbol
        };
    }
}
