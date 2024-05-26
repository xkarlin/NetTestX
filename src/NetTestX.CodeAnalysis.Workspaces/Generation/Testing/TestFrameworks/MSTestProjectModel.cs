using System.Collections.Generic;
using NetTestX.Common;

namespace NetTestX.CodeAnalysis.Workspaces.Generation.Testing.TestFrameworks;

/// <summary>
/// <see cref="ITestFrameworkProjectModel"/> for MSTest
/// </summary>
public class MSTestProjectModel : ITestFrameworkProjectModel
{
    public TestFramework Framework => TestFramework.MSTest;

    public IEnumerable<PackageReference> PackageReferences { get; } =
    [
        new("MSTest.TestAdapter"),
        new("MSTest.TestFramework")
    ];
}
