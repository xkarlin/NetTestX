using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace NetTestX.VSIX.Options.Parsing;

public class GeneralOptionsVariablesProvider(INamedTypeSymbol type) : IParsingVariablesProvider
{
    private const string TEST_CLASS_V = "TypeName";

    private const string TEST_CLASS_NAMESPACE_V = "TypeNamespace";

    private readonly Dictionary<string, string> _variables = new()
    {
        [TEST_CLASS_V] = type.Name,
        [TEST_CLASS_NAMESPACE_V] = type.ContainingNamespace.ToDisplayString()
    };

    public IReadOnlyCollection<string> Variables => _variables.Keys;

    public string ResolveVariable(string variable) => _variables[variable];
}
