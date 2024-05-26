using System.Collections.Generic;
using NetTestX.Common;

namespace NetTestX.CodeAnalysis.Workspaces.Generation.Testing.MockingLibraries;

/// <summary>
/// Represents a data model for the given <see cref="Library"/> that contains necessary
/// information used during project generation
/// </summary>
public interface IMockingLibraryProjectModel
{
    /// <summary>
    /// The <see cref="MockingLibrary"/> that this model uses
    /// </summary>
    MockingLibrary Library { get; }

    /// <summary>
    /// All package references needed by this <see cref="Library"/>
    /// </summary>
    IEnumerable<PackageReference> PackageReferences { get; }
}
