using System.Threading.Tasks;
using Microsoft.CodeAnalysis;

namespace NetTestX.VSIX.Code.TypeSymbolProviders;

public interface ITypeSymbolProvider
{
    Task<INamedTypeSymbol> GetTypeSymbolAsync();
}
