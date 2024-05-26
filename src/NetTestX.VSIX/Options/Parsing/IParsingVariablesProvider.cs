using System.Collections.Generic;

namespace NetTestX.VSIX.Options.Parsing;

/// <summary>
/// Represents a provider that is capable of replacing variables with actual values
/// </summary>
public interface IParsingVariablesProvider
{
    /// <summary>
    /// The list of variables that this provider knows
    /// </summary>
    IReadOnlyCollection<string> Variables { get; }

    /// <summary>
    /// Resolve the provided <paramref name="variable"/> with the actual value
    /// </summary>
    string ResolveVariable(string variable);
}
