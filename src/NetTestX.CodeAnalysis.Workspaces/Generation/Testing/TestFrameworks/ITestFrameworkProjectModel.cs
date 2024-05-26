using System.Collections.Generic;
using NetTestX.Common;

namespace NetTestX.CodeAnalysis.Workspaces.Generation.Testing.TestFrameworks;

/// <summary>
/// Represents a data model for test frameworks that contains all necessary information
/// used during project generation
/// </summary>
public interface ITestFrameworkProjectModel
{
    /// <summary>
    /// The <see cref="TestFramework"/> that this model represents
    /// </summary>
    TestFramework Framework { get; }

    /// <summary>
    /// All package references needed by this <see cref="Framework"/>
    /// </summary>
    IEnumerable<PackageReference> PackageReferences { get; }
}
