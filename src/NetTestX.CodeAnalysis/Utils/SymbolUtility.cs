using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Common;
using NetTestX.CodeAnalysis.Extensions;
using System.Linq;
using System.Text;

namespace NetTestX.CodeAnalysis.Utils;

/// <summary>
/// Helper class used to stringify <see cref="ISymbol"/>s and derived types
/// </summary>
public static class SymbolUtility
{
    /// <summary>
    /// Get a display name for the given <paramref name="symbol"/>
    /// </summary>
    public static string GetDisplayName(ISymbol symbol) => symbol switch
    {
        IMethodSymbol method => GetDisplayName(method),
        IPropertySymbol property => GetDisplayName(property),
        _ => symbol.ToDisplayString()
    };

    /// <summary>
    /// Get a display name for the given <paramref name="method"/>
    /// </summary>
    public static string GetDisplayName(IMethodSymbol method)
    {
        StringBuilder builder = new();

        var accessibility = method.GetEffectiveAccessibility();
        
        builder.Append(accessibility.ToString().ToLowerInvariant());
        builder.Append(' ');
        
        if (method.IsStatic)
            builder.Append("static ");

        if (method.IsAsync)
            builder.Append("async ");

        builder.Append(method.ReturnType.ToDisplayString(CommonFormats.NameOnlyGenericFormat));
        builder.Append(' ');

        builder.Append(method.ToDisplayString(CommonFormats.NameOnlyGenericFormat));

        builder.Append('(');
        builder.Append(string.Join(", ", method.Parameters.Select(x => x.Type.ToDisplayString(CommonFormats.NameOnlyGenericFormat))));
        builder.Append(')');

        return builder.ToString();
    }

    /// <summary>
    /// Get a display name for the given <paramref name="property"/>
    /// </summary>
    public static string GetDisplayName(IPropertySymbol property)
    {
        if (!property.IsIndexer)
            return property.ToDisplayString();

        StringBuilder builder = new();

        var accessibility = property.GetEffectiveAccessibility();

        builder.Append(accessibility.ToString().ToLowerInvariant());
        builder.Append(' ');

        builder.Append(property.Type.ToDisplayString(CommonFormats.NameOnlyGenericFormat));
        builder.Append(' ');

        builder.Append("this");

        builder.Append('[');
        builder.Append(string.Join(", ", property.Parameters.Select(x => x.Type.ToDisplayString(CommonFormats.NameOnlyGenericFormat))));
        builder.Append(']');

        return builder.ToString();
    }
}
