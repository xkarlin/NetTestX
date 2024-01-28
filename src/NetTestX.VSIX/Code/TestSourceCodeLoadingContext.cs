using EnvDTE80;
using NetTestX.VSIX.Code.TypeSymbolProviders;

namespace NetTestX.VSIX.Code;

public readonly struct TestSourceCodeLoadingContext
{
    public required DTE2 DTE { get; init; }

    public ITypeSymbolProvider TypeSymbolProvider { get; init; }
}
