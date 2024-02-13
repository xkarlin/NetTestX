using Microsoft.AspNetCore.Razor.Language.Intermediate;
using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Common;
using NetTestX.CodeAnalysis.Extensions;
using NetTestX.CodeAnalysis.Generics.Constraints;
using NetTestX.CodeAnalysis.Utils;
using NetTestX.Polyfill.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;

namespace NetTestX.CodeAnalysis.Generics;

internal static class GenericTypeResolver
{
    private static readonly ConditionalWeakTable<Compilation, INamedTypeSymbol[]> _wellKnownTypesCache = new();

    public static ITypeSymbol Resolve(ITypeSymbol type, Compilation compilation)
        => type.Kind == SymbolKind.NamedType ? Resolve((INamedTypeSymbol)type, compilation) : type;

    public static INamedTypeSymbol Resolve(INamedTypeSymbol type, Compilation compilation)
    {
        if (!type.IsGenericTypeDefinition())
            return type;

        var typeArguments = ResolveGenericTypeArguments(type, compilation);
        type = ConstructGenericType(type, typeArguments);
        return type;
    }

    private static IReadOnlyDictionary<ITypeParameterSymbol, ITypeSymbol> ResolveGenericTypeArguments(INamedTypeSymbol type, Compilation compilation)
    {
        Dictionary<ITypeParameterSymbol, ITypeSymbol> resolvedTypeArguments = new(SymbolEqualityComparer.Default);
        Dictionary<ITypeParameterSymbol, IConstraint> defaultConstraints = new(SymbolNameComparer.Default);

        foreach (var typeParameter in type.TypeParameters)
        {
            CompositeConstraint constraint = new(ConstraintHelper.GetTypeParameterConstraintsWithoutTypes(typeParameter));
            defaultConstraints.Add(typeParameter, constraint);
        }

        ResolveGenericTypeArgumentsInternal(resolvedTypeArguments, defaultConstraints, type, compilation);

        foreach (var typeParameter in type.TypeParameters)
        {
            if (!resolvedTypeArguments.ContainsKey(typeParameter))
                resolvedTypeArguments.Add(typeParameter, compilation.GetSpecialType(SpecialType.System_Void));
        }

        return resolvedTypeArguments;
    }
    
    private static void ResolveGenericTypeArgumentsInternal(Dictionary<ITypeParameterSymbol, ITypeSymbol> resolvedTypeArguments, Dictionary<ITypeParameterSymbol, IConstraint> constraints, INamedTypeSymbol type, Compilation compilation)
    {
        HashSet<ITypeParameterSymbol> visited = new(SymbolEqualityComparer.Default);

        for (int i = 0; i < type.Arity; i++)
        {
            Dictionary<ITypeParameterSymbol, ITypeSymbol> resolvedArguments = new(resolvedTypeArguments, SymbolEqualityComparer.Default);

            if (!visited.Add(type.TypeParameters[i]))
                continue;

            if (ResolveGenericTypeArgumentInternal(type.TypeParameters[i], resolvedArguments, constraints, compilation) is { } nextResolvedArguments)
            {
                foreach (var (p, r) in nextResolvedArguments)
                {
                    visited.Add(p);
                    resolvedTypeArguments[p] = r;
                }
            }
        }
    }

    private static IDictionary<ITypeParameterSymbol, ITypeSymbol> ResolveGenericTypeArgumentInternal(ITypeParameterSymbol typeParameter, IDictionary<ITypeParameterSymbol, ITypeSymbol> resolvedTypeArguments, IDictionary<ITypeParameterSymbol, IConstraint> constraints, Compilation compilation)
    {
        if (resolvedTypeArguments.TryGetValue(typeParameter, out _))
            return resolvedTypeArguments;

        foreach (var candidateType in TypeArgumentSuggester.EnumerateSuggestions(compilation))
        {
            Dictionary<ITypeParameterSymbol, ITypeSymbol> nextResolvedTypeArguments = new(resolvedTypeArguments, SymbolEqualityComparer.Default);
            Dictionary<ITypeParameterSymbol, ITypeSymbol> resolvedCandidateTypeArguments = new(SymbolEqualityComparer.Default);
            Dictionary<ITypeParameterSymbol, IConstraint> nextConstraints = new(constraints, SymbolEqualityComparer.Default);

            Dictionary<ITypeParameterSymbol, ITypeParameterSymbol> typeParameterMapping = new(SymbolEqualityComparer.Default);

            bool constraintTypesResolved = true;

            foreach (INamedTypeSymbol constraintType in typeParameter.ConstraintTypes)
            {
                bool constraintTypeResolved = false;

                foreach (var candidateIface in candidateType.FindAllGenericInterfaceImplementations(constraintType))
                {
                    if (TryResolveCandidateConstraintType(candidateIface, constraintType, nextResolvedTypeArguments, resolvedCandidateTypeArguments, nextConstraints, typeParameterMapping, compilation))
                    {
                        constraintTypeResolved = true;
                        break;
                    }
                }

                if (!constraintTypeResolved)
                {
                    constraintTypesResolved = false;
                    break;
                }
            }

            if (!constraintTypesResolved)
                continue;

            if (!nextConstraints[typeParameter].IsSatisfiedBy(candidateType))
                continue;

            if (!candidateType.IsGenericType)
            {
                nextResolvedTypeArguments[typeParameter] = candidateType;
                return nextResolvedTypeArguments;
            }

            Dictionary<ITypeParameterSymbol, ITypeSymbol> candidateTypeArguments = new(SymbolEqualityComparer.Default);
            Dictionary<ITypeParameterSymbol, IConstraint> candidateTypeConstraints = new(SymbolNameComparer.Default);

            for (int i = 0; i < candidateType.Arity; i++)
            {
                if (resolvedCandidateTypeArguments.TryGetValue(candidateType.TypeParameters[i], out var resolvedCandidateTypeArgument))
                {
                    candidateTypeArguments[candidateType.TypeParameters[i]] = resolvedCandidateTypeArgument;
                    continue;
                }

                if (typeParameterMapping.TryGetValue(candidateType.TypeParameters[i], out var mappedArgument))
                {
                    candidateTypeArguments[candidateType.TypeParameters[i]] = nextResolvedTypeArguments[mappedArgument];
                    continue;
                }
                   
                var constraint = new CompositeConstraint(ConstraintHelper.GetTypeParameterConstraintsWithoutTypes(candidateType.TypeParameters[i]));
                candidateTypeConstraints[candidateType.TypeParameters[i]] = constraint;
            }

            ResolveGenericTypeArgumentsInternal(candidateTypeArguments, candidateTypeConstraints, candidateType, compilation);
            var constructedCandidateType = ConstructGenericType(candidateType, candidateTypeArguments);
            nextResolvedTypeArguments[typeParameter] = constructedCandidateType;
            return nextResolvedTypeArguments;
        }

        return null;
    }

    private static bool TryResolveCandidateConstraintType(
        INamedTypeSymbol candidateType,
        INamedTypeSymbol constraintType,
        IDictionary<ITypeParameterSymbol, ITypeSymbol> resolvedTypeArguments,
        IDictionary<ITypeParameterSymbol, ITypeSymbol> resolvedCandidateTypeArguments,
        IDictionary<ITypeParameterSymbol, IConstraint> constraints,
        Dictionary<ITypeParameterSymbol, ITypeParameterSymbol> typeParameterMapping,
        Compilation compilation)
    {
        Dictionary<ITypeParameterSymbol, IConstraint> nextConstraints = new(constraints, SymbolEqualityComparer.Default);

        for (int i = 0; i < candidateType.Arity; i++)
        {
            if (constraintType.TypeArguments[i] is INamedTypeSymbol t)
            {
                resolvedCandidateTypeArguments[(ITypeParameterSymbol)candidateType.TypeArguments[i]] = t;
                continue;
            }

            var currentConstraint = constraints[(ITypeParameterSymbol)constraintType.TypeArguments[i]];

            IConstraint candidateConstraint;

            if (candidateType.TypeArguments[i] is ITypeParameterSymbol p)
            {
                if (resolvedCandidateTypeArguments.TryGetValue(p, out var resolvedCandidateTypeArgument))
                {
                    candidateConstraint = new EqualsTypeConstraint(resolvedCandidateTypeArgument);
                }
                else if (typeParameterMapping.TryGetValue(p, out var resolvedTypeParameter) && resolvedTypeArguments.TryGetValue(resolvedTypeParameter, out var resolvedTypeArgument1))
                {
                    candidateConstraint = new EqualsTypeConstraint(resolvedTypeArgument1);
                }
                else
                {
                    candidateConstraint = new CompositeConstraint(ConstraintHelper.GetTypeParameterConstraintsWithoutTypes(p));
                    typeParameterMapping[p] = (ITypeParameterSymbol)constraintType.TypeArguments[i];
                }
            }
            else
            {
                candidateConstraint = new EqualsTypeConstraint(candidateType.TypeArguments[i]);
            }

            nextConstraints[(ITypeParameterSymbol)constraintType.TypeArguments[i]] = new CompositeConstraint([currentConstraint, candidateConstraint]);

            if (ResolveGenericTypeArgumentInternal((ITypeParameterSymbol)constraintType.TypeArguments[i], resolvedTypeArguments, nextConstraints, compilation) is { } nextResolvedTypeArguments)
            {
                resolvedTypeArguments[(ITypeParameterSymbol)constraintType.TypeArguments[i]] = nextResolvedTypeArguments[(ITypeParameterSymbol)constraintType.TypeArguments[i]];
            }
            else
            {
                resolvedTypeArguments.Clear();
                return false;
            }
        }

        return true;
    }

    private static INamedTypeSymbol ConstructGenericType(INamedTypeSymbol type, IReadOnlyDictionary<ITypeParameterSymbol, ITypeSymbol> typeArguments)
    {
        var typeArgumentsOrdered = new ITypeSymbol[type.Arity];

        for (int i = 0; i < type.Arity; i++)
        {
            var typeParameter = type.TypeParameters[i];
            var typeArgument = typeArguments[typeParameter];
            typeArgumentsOrdered[i] = typeArgument;
        }

        type = type.Construct(typeArgumentsOrdered);
        return type;
    }
}
