using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Common;
using NetTestX.CodeAnalysis.Generation.TypeValueProviders;

namespace NetTestX.CodeAnalysis.Generation.TypeValueProviders;

public class NSubstituteValueProvider : TypeValueProviderBase
{
    public override string Resolve(ITypeSymbol type)
    {
        if (type.TypeKind == TypeKind.Interface)
            return $"Substitute.For<{type.ToDisplayString(CommonFormats.FullNullableFormat)}>()";

        return base.Resolve(type);
    }
}
