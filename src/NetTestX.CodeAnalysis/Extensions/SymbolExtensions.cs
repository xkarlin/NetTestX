using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace NetTestX.CodeAnalysis.Extensions;

public static class SymbolExtensions
{
    public static bool IsNumericType(this ITypeSymbol type) => type is
    {
        SpecialType: SpecialType.System_SByte or
            SpecialType.System_Byte or
            SpecialType.System_Int16 or
            SpecialType.System_UInt16 or
            SpecialType.System_Int32 or
            SpecialType.System_UInt32 or
            SpecialType.System_Int64 or
            SpecialType.System_UInt64 or
            SpecialType.System_Decimal or
            SpecialType.System_Single or
            SpecialType.System_Double
    };

    public static IEnumerable<string> CollectNamespaces(this ISymbol symbol)
    {
        HashSet<string> namespaces = [];

        CollectNamespacesInternal(symbol);

        return namespaces;

        void CollectNamespacesInternal(ISymbol current)
        {
            if (current.ContainingNamespace is { } containingNs)
                namespaces.Add(containingNs.ToDisplayString());

            if (current.ContainingType is { } containingType)
                CollectNamespacesInternal(containingType);

            if (current is IArrayTypeSymbol array)
                CollectNamespacesInternal(array.ElementType);

            if (current is INamedTypeSymbol { IsGenericType: true } genericType)
            {
                foreach (var typeArgument in genericType.TypeArguments)
                    CollectNamespacesInternal(typeArgument);
            }
        }
    }
}
