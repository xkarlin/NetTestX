namespace NetTestX.CodeAnalysis.Workspaces.Projects;

public class PackageReference
{
    public string Name { get; }

    public bool DevelopmentOnly { get; }

    public PackageReference(string name, bool developmentOnly = false)
    {
        Name = name;
        DevelopmentOnly = developmentOnly;
    }
}
