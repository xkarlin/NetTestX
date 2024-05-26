using Microsoft.CodeAnalysis;

namespace NetTestX.CodeAnalysis.Common;

/// <summary>
/// Helper class that contains common <see cref="SymbolDisplayFormat"/> instances
/// </summary>
public static class CommonFormats
{
    /// <summary>
    /// <see cref="SymbolDisplayFormat"/> that has full type name and includes NRT modifiers
    /// </summary>
    public static SymbolDisplayFormat FullNullableFormat { get; } = new(
        typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces,
        globalNamespaceStyle: SymbolDisplayGlobalNamespaceStyle.Omitted,
        genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters,
        miscellaneousOptions: SymbolDisplayMiscellaneousOptions.IncludeNullableReferenceTypeModifier |
            SymbolDisplayMiscellaneousOptions.UseSpecialTypes);

    /// <summary>
    /// <see cref="SymbolDisplayFormat"/> that has short type name and includes NRT modifiers
    /// </summary>
    public static SymbolDisplayFormat ShortNullableFormat { get; } = new(
        typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypes,
        globalNamespaceStyle: SymbolDisplayGlobalNamespaceStyle.Omitted,
        genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters,
        miscellaneousOptions: SymbolDisplayMiscellaneousOptions.IncludeNullableReferenceTypeModifier |
            SymbolDisplayMiscellaneousOptions.UseSpecialTypes);

    /// <summary>
    /// <see cref="SymbolDisplayFormat"/> that has type name only
    /// </summary>
    public static SymbolDisplayFormat NameOnlyFormat { get; } = new(
        typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameOnly,
        globalNamespaceStyle: SymbolDisplayGlobalNamespaceStyle.Omitted,
        genericsOptions: SymbolDisplayGenericsOptions.None,
        miscellaneousOptions: SymbolDisplayMiscellaneousOptions.UseSpecialTypes);

    /// <summary>
    /// <see cref="SymbolDisplayFormat"/> that has type name only without using special types (int, char etc.)
    /// </summary>
    public static SymbolDisplayFormat NameOnlyFormatNoSpecialTypes { get; } = new(
        typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameOnly,
        globalNamespaceStyle: SymbolDisplayGlobalNamespaceStyle.Omitted,
        genericsOptions: SymbolDisplayGenericsOptions.None);

    /// <summary>
    /// <see cref="SymbolDisplayFormat"/> that has only type name and generic type parameters
    /// </summary>
    public static SymbolDisplayFormat NameOnlyGenericFormat { get; } = new(
        typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameOnly,
        globalNamespaceStyle: SymbolDisplayGlobalNamespaceStyle.Omitted,
        genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeConstraints,
        miscellaneousOptions: SymbolDisplayMiscellaneousOptions.UseSpecialTypes);
}
