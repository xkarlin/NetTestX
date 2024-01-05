using System.Collections.Generic;
using NetTestX.Common;

namespace NetTestX.CodeAnalysis.Workspaces.Projects.Testing.TestFrameworks;

public class NUnitProjectModel : ITestFrameworkProjectModel
{
    public TestFramework Framework => TestFramework.NUnit;

    public IEnumerable<PackageReference> PackageReferences =>
    [
        new("NUnit"),
        new("NUnit3TestAdapter"),
        new("NUnit.Analyzers")
    ];
}
