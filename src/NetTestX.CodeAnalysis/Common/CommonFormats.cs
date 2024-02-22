using Microsoft.CodeAnalysis;

namespace NetTestX.CodeAnalysis.Common;

public static class CommonFormats
{
    public static SymbolDisplayFormat FullNullableFormat { get; } = new(
        typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces,
        globalNamespaceStyle: SymbolDisplayGlobalNamespaceStyle.Omitted,
        genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters,
        miscellaneousOptions: SymbolDisplayMiscellaneousOptions.IncludeNullableReferenceTypeModifier |
            SymbolDisplayMiscellaneousOptions.UseSpecialTypes);

    public static SymbolDisplayFormat ShortNullableFormat { get; } = new(
        typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypes,
        globalNamespaceStyle: SymbolDisplayGlobalNamespaceStyle.Omitted,
        genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters,
        miscellaneousOptions: SymbolDisplayMiscellaneousOptions.IncludeNullableReferenceTypeModifier |
            SymbolDisplayMiscellaneousOptions.UseSpecialTypes);

    public static SymbolDisplayFormat NameOnlyFormat { get; } = new(
        typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameOnly,
        globalNamespaceStyle: SymbolDisplayGlobalNamespaceStyle.Omitted,
        genericsOptions: SymbolDisplayGenericsOptions.None,
        miscellaneousOptions: SymbolDisplayMiscellaneousOptions.UseSpecialTypes);

    public static SymbolDisplayFormat NameOnlyGenericFormat { get; } = new(
        typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameOnly,
        globalNamespaceStyle: SymbolDisplayGlobalNamespaceStyle.Omitted,
        genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters,
        miscellaneousOptions: SymbolDisplayMiscellaneousOptions.UseSpecialTypes);
}
