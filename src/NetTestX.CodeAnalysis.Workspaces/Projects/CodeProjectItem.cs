using System;

namespace NetTestX.CodeAnalysis.Workspaces.Projects;

public class CodeProjectItem(string name, string include) : IEquatable<CodeProjectItem>
{
    public string Name { get; } = name;

    public string Include { get; } = include;

    public bool Equals(CodeProjectItem other)
    {
        if (other is null)
            return false;

        return Name == other.Name && Include == other.Include;
    }
}
