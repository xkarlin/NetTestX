using Microsoft.CodeAnalysis;
using NetTestX.CodeAnalysis.Common;
using System.Collections.Generic;
using System.Linq;

namespace NetTestX.CodeAnalysis.Generation.TypeValueProviders;

public class NSubstituteValueProvider : TypeValueProviderBase
{
    public const string NSUBSTITUTE_N = "NSubstitute";

    public override string Resolve(ITypeSymbol type)
    {
        if (type.TypeKind == TypeKind.Interface)
            return $"Substitute.For<{type.ToDisplayString(CommonFormats.ShortNullableFormat)}>()";

        return base.Resolve(type);
    }

    public override IEnumerable<string> CollectNamespaces() => base.CollectNamespaces().Union([NSUBSTITUTE_N]);
}
