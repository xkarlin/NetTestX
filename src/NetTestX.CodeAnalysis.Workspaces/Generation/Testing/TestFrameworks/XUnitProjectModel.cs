using System.Collections.Generic;
using NetTestX.Common;

namespace NetTestX.CodeAnalysis.Workspaces.Generation.Testing.TestFrameworks;

public class XUnitProjectModel : ITestFrameworkProjectModel
{
    public TestFramework Framework => TestFramework.XUnit;

    public IEnumerable<PackageReference> PackageReferences =>
    [
        new("xunit"),
        new("xunit.runner.visualstudio", true)
    ];
}
