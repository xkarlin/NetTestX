namespace NetTestX.CodeAnalysis.Workspaces.Generation;

public class PackageReference(string name, bool developmentOnly = false)
{
    public string Name { get; } = name;

    public bool DevelopmentOnly { get; } = developmentOnly;
}
