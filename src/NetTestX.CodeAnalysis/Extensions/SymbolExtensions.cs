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
}
