using System;

namespace NetTestX.CodeAnalysis.Workspaces.Generation;

/// <summary>
/// Represents a package reference used by a <see cref="Projects.CodeProject"/>
/// </summary>
public class PackageReference(string name, bool developmentOnly = false) : IEquatable<PackageReference>
{
    /// <summary>
    /// The name of the package
    /// </summary>
    public string Name { get; } = name;

    /// <summary>
    /// Whether the package is development only (should be omitted in prod builds)
    /// </summary>
    public bool DevelopmentOnly { get; } = developmentOnly;

    public bool Equals(PackageReference other)
    {
        if (other is null)
            return false;

        return Name == other.Name && DevelopmentOnly == other.DevelopmentOnly;
    }
}
