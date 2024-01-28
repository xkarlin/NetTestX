using System.Threading.Tasks;
using Microsoft.CodeAnalysis;

namespace NetTestX.VSIX.Code.TypeSymbolProviders;

public class DefaultTypeSymbolProvider(INamedTypeSymbol typeSymbol) : ITypeSymbolProvider
{
    public Task<INamedTypeSymbol> GetTypeSymbolAsync() => Task.FromResult(typeSymbol);
}
