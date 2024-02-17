namespace NetTestX.VSIX.Options.Parsing;

public class OptionResolver(IParsingVariablesProvider provider)
{
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
