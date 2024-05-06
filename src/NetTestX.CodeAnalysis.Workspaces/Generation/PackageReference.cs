using System;

namespace NetTestX.CodeAnalysis.Workspaces.Generation;

public class PackageReference(string name, bool developmentOnly = false) : IEquatable<PackageReference>
{
    public string Name { get; } = name;

    public bool DevelopmentOnly { get; } = developmentOnly;

    public bool Equals(PackageReference other)
    {
        if (other is null)
            return false;

        return Name == other.Name && DevelopmentOnly == other.DevelopmentOnly;
    }
}
