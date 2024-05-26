using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Common;

namespace NetTestX.CodeAnalysis.Extensions;

/// <summary>
/// Extensions for <see cref="ISymbol"/> and derivatives
/// </summary>
public static class SymbolExtensions
{
    /// <summary>
    /// Whether the provided <see cref="ITypeSymbol"/> represents a numeric type
    /// </summary>
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

    /// <summary>
    /// Whether the provided <see cref="ITypeSymbol"/> implements the given interface (<typeparamref name="T"/>)
    /// </summary>
    public static bool ImplementsInterface<T>(this ITypeSymbol type)
        where T : class
    {
        // naive impl, this is not perfect
        return type.AllInterfaces.Any(x => x.Name == typeof(T).Name);
    }

    /// <summary>
    /// Find all members of the given <see cref="INamedTypeSymbol"/> that implement some member of interface <typeparamref name="T"/>
    /// </summary>
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

    /// <summary>
    /// Whether the given <see cref="ITypeSymbol"/> represents a generic type definition
    /// </summary>
    public static bool IsGenericTypeDefinition(this ITypeSymbol type)
    {
        if (type.Kind != SymbolKind.NamedType)
            return false;

        return ((INamedTypeSymbol)type) is { IsGenericType: true } named && SymbolNameComparer.Default.Equals(named, named.OriginalDefinition);
    }

    /// <summary>
    /// Whether the given <see cref="IMethodSymbol"/> represents a generic method definition
    /// </summary>
    public static bool IsGenericMethodDefinition(this IMethodSymbol method)
    {
        return method.IsGenericMethod && SymbolNameComparer.Default.Equals(method, method.OriginalDefinition);
    }

    /// <summary>
    /// Whether the given <see cref="ITypeSymbol"/> implements the specified <paramref name="iface"/>
    /// </summary>
    public static bool ImplementsInterface(this ITypeSymbol type, INamedTypeSymbol iface)
    {
        return type.AllInterfaces.Any(x => SymbolNameComparer.Default.Equals(x, iface));
    }

    /// <summary>
    /// Whether the given <see cref="ITypeSymbol"/> implements the specified generic <paramref name="iface"/>
    /// </summary>
    public static bool ImplementsGenericInterface(this ITypeSymbol type, INamedTypeSymbol iface)
    {
        return type.AllInterfaces.Any(x => SymbolNameComparer.Default.Equals(x.OriginalDefinition, iface));
    }

    /// <summary>
    /// Find all members of the given <see cref="INamedTypeSymbol"/> that implement some member of a generic interface <paramref name="iface"/>
    /// </summary>
    public static IEnumerable<INamedTypeSymbol> FindAllGenericInterfaceImplementations(this ITypeSymbol type, INamedTypeSymbol iface)
    {
        foreach (var candidateIface in type.AllInterfaces)
        {
            if (SymbolNameComparer.Default.Equals(candidateIface.OriginalDefinition, iface.OriginalDefinition))
                yield return candidateIface;
        }
    }

    /// <summary>
    /// Whether the given <see cref="ITypeSymbol"/> is inherited from <paramref name="baseType"/>
    /// </summary>
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

    /// <summary>
    /// Whether the given <see cref="ITypeSymbol"/> is inherited from a generic <paramref name="baseType"/>
    /// </summary>
    public static bool IsInheritedFromGenericType(this ITypeSymbol type, INamedTypeSymbol baseType)
    {
        if (type is null)
            return false;

        if (SymbolNameComparer.Default.Equals(type.OriginalDefinition, baseType))
            return true;

        return IsInheritedFromGenericType(type.BaseType, baseType);
    }

    /// <summary>
    /// Whether the given <see cref="INamedTypeSymbol"/> has any accessible constructor (taking into account <paramref name="accessibility"/>)
    /// </summary>
    public static bool HasAccessibleConstructor(this INamedTypeSymbol type, Accessibility accessibility = Accessibility.Public)
        => TryGetAccessibleConstructor(type, accessibility, out _);

    /// <summary>
    /// Try to get any accessible constructor for the given <see cref="INamedTypeSymbol"/> (taking into account <paramref name="accessibility"/>)
    /// </summary>
    public static bool TryGetAccessibleConstructor(this INamedTypeSymbol type, Accessibility accessibility, out IMethodSymbol constructor)
    {
        constructor = type.Constructors.FirstOrDefault(x => x.DeclaredAccessibility >= accessibility);
        return constructor is not null;
    }

    /// <summary>
    /// Get the effective accessibility for the given <see cref="ISymbol"/>
    /// </summary>
    /// <remarks>
    /// This takes into account its containing type visibility (recursively) to determine the
    /// "actual" accessibility that the symbol has
    /// </remarks>
    public static Accessibility GetEffectiveAccessibility(this ISymbol symbol)
    {
        var accessibility = symbol.DeclaredAccessibility switch
        {
            Accessibility.NotApplicable => Accessibility.Private,
            var x => x
        };

        if (symbol.ContainingType is { } containingType)
        {
            var parentAccessibility = containingType.GetEffectiveAccessibility();

            if (parentAccessibility < accessibility)
                accessibility = parentAccessibility;
        }

        return accessibility;
    }

    /// <summary>
    /// Collect all namespaces that are used by the given <see cref="ISymbol"/>.
    /// Putting all of them at the top of the file allows to use this symbol in code
    /// without compilation issues
    /// </summary>
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
