using System;

namespace NetTestX.CodeAnalysis.Workspaces.Projects;

/// <summary>
/// Represents an MSBuild item inside a <see cref="CodeProject"/>
/// </summary>
public class CodeProjectItem(string name, string include) : IEquatable<CodeProjectItem>
{
    /// <summary>
    /// The name of the item
    /// </summary>
    public string Name { get; } = name;

    /// <summary>
    /// The Include tag of the item
    /// </summary>
    public string Include { get; } = include;

    public bool Equals(CodeProjectItem other)
    {
        if (other is null)
            return false;

        return Name == other.Name && Include == other.Include;
    }
}
