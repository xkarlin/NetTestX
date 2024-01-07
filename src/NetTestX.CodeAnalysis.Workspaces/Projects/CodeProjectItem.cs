namespace NetTestX.CodeAnalysis.Workspaces.Projects;

public class CodeProjectItem(string name, string include)
{
    public string Name { get; } = name;

    public string Include { get; } = include;
}
