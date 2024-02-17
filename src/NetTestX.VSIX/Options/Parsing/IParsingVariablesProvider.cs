using System.Collections.Generic;

namespace NetTestX.VSIX.Options.Parsing;

public interface IParsingVariablesProvider
{
    IReadOnlyCollection<string> Variables { get; }

    string ResolveVariable(string variable);
}
