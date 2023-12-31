using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Common;
using NetTestX.CodeAnalysis.Extensions;
using System.Collections.Generic;

namespace NetTestX.CodeAnalysis.Generation.TypeValueProviders;

public abstract class TypeValueProviderBase : ITypeValueProvider
{
    public virtual string Resolve(ITypeSymbol type) => type switch
    {
        IArrayTypeSymbol a => ResolveArray(a),
        INamedTypeSymbol n => ResolveNamed(n),
        _ => Default(type)
    };

    public virtual IEnumerable<string> CollectNamespaces() => [];

    protected string Default(ITypeSymbol type) => $"default({type.ToDisplayString(CommonFormats.ShortNullableFormat)})";

    private string ResolveArray(IArrayTypeSymbol array) => $"Array.Empty<{array.ElementType.ToDisplayString(CommonFormats.ShortNullableFormat)}>()";

    private string ResolveNamed(INamedTypeSymbol named) => named switch
    {
        var x when x.IsNumericType() => "0",
        { SpecialType: SpecialType.System_Boolean } => "false",
        { SpecialType: SpecialType.System_Char } => "' '",
        { SpecialType: SpecialType.System_String } => @"""""",
        _ => Default(named)
    };
 }
