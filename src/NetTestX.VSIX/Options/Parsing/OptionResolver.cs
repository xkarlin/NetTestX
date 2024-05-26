namespace NetTestX.VSIX.Options.Parsing;

/// <summary>
/// Class used to resolve strings that contain option variables
/// </summary>
public class OptionResolver(IParsingVariablesProvider provider)
{
    /// <summary>
    /// Resolve the passed <paramref name="text"/> that may contain option variables
    /// </summary>
    public string Resolve(string text)
    {
        foreach (var variable in provider.Variables)
        {
            string resolvedVariable = provider.ResolveVariable(variable);
            string templateVariable = $$"""{{{variable}}}""";
            text = text.Replace(templateVariable, resolvedVariable);
        }

        return text;
    }
}
