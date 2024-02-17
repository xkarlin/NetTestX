using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Common;

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

    public static bool IsGenericTypeDefinition(this ITypeSymbol type)
    {
        if (type.Kind != SymbolKind.NamedType)
            return false;

        return ((INamedTypeSymbol)type) is { IsGenericType: true } named && SymbolNameComparer.Default.Equals(named, named.OriginalDefinition);
    }

    public static bool IsGenericMethodDefinition(this IMethodSymbol method)
    {
        return method.IsGenericMethod && SymbolNameComparer.Default.Equals(method, method.OriginalDefinition);
    }

    public static bool ImplementsInterface(this ITypeSymbol type, INamedTypeSymbol iface)
    {
        return type.AllInterfaces.Any(x => SymbolNameComparer.Default.Equals(x, iface));
    }

    public static bool ImplementsGenericInterface(this ITypeSymbol type, INamedTypeSymbol iface)
    {
        return type.AllInterfaces.Any(x => SymbolNameComparer.Default.Equals(x.OriginalDefinition, iface));
    }

    public static IEnumerable<INamedTypeSymbol> FindAllGenericInterfaceImplementations(this ITypeSymbol type, INamedTypeSymbol iface)
    {
        foreach (var candidateIface in type.AllInterfaces)
        {
            if (SymbolNameComparer.Default.Equals(candidateIface.OriginalDefinition, iface.OriginalDefinition))
                yield return candidateIface;
        }
    }

    public static bool IsInheritedFrom(this ITypeSymbol type, ITypeSymbol baseType)
    {
        if (!type.IsReferenceType)
            return false;

        if (type is null)
            return false;

        if (SymbolNameComparer.Default.Equals(type, baseType))
            return true;

        return IsInheritedFrom(type.BaseType, baseType);
    }

    public static bool IsInheritedFromGenericType(this ITypeSymbol type, INamedTypeSymbol baseType)
    {
        if (type is null)
            return false;

        if (SymbolNameComparer.Default.Equals(type.OriginalDefinition, baseType))
            return true;

        return IsInheritedFromGenericType(type.BaseType, baseType);
    }

    public static bool HasAccessibleConstructor(this INamedTypeSymbol type, Accessibility accessibility = Accessibility.Public)
        => TryGetAccessibleConstructor(type, accessibility, out _);

    public static bool TryGetAccessibleConstructor(this INamedTypeSymbol type, Accessibility accessibility, out IMethodSymbol constructor)
    {
        constructor = type.Constructors.FirstOrDefault(x => x.DeclaredAccessibility >= accessibility);
        return constructor is not null;
    }

    public static IEnumerable<string> CollectNamespaces(this ISymbol symbol)
    {
        HashSet<string> namespaces = [];

        CollectNamespacesInternal(symbol);

        return namespaces;

        void CollectNamespacesInternal(ISymbol current)
        {
            if (current is ITypeParameterSymbol)
                return;

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
