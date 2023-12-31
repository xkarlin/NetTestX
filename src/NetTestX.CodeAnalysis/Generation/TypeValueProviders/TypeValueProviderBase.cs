using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Extensions;

namespace NetTestX.CodeAnalysis.Generation.DependencyResolvers;

public abstract class TypeValueProviderBase : ITypeValueProvider
{
    public virtual string Resolve(ITypeSymbol type) => type switch
    {
        IArrayTypeSymbol a => ResolveArray(a),
        INamedTypeSymbol n => ResolveNamed(n),
        _ => "default"
    };

    private string ResolveArray(IArrayTypeSymbol array) => "[]";

    private string ResolveNamed(INamedTypeSymbol named) => named switch
    {
        var x when x.IsNumericType() => "0",
        { SpecialType: SpecialType.System_Boolean } => "false",
        { SpecialType: SpecialType.System_Char } => "' '",
        { SpecialType: SpecialType.System_String } => @"""""",
        _ => "default"
    };
 }
