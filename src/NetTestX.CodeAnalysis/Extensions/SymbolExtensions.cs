using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

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

    public static bool ImplementsInterface<T>(this ITypeSymbol type)
        where T : class
    {
        // naive impl, this is not perfect
        return type.AllInterfaces.Any(x => x.Name == typeof(T).Name);
    }

    public static IEnumerable<ISymbol> FindImplementationsForInterfaceMembers<T>(this INamedTypeSymbol typeSymbol, Compilation compilation)
        where T : class
    {
        var ifaceSymbol = compilation.GetNamedSymbol<T>();

        foreach (var member in ifaceSymbol.GetMembers())
        {
            if (typeSymbol.FindImplementationForInterfaceMember(member) is { } impl)
                yield return impl;
        }
    }

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
