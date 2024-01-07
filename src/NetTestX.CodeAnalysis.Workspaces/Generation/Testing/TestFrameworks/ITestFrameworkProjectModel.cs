using System.Collections.Generic;
using NetTestX.Common;

namespace NetTestX.CodeAnalysis.Workspaces.Generation.Testing.TestFrameworks;

public interface ITestFrameworkProjectModel
{
    TestFramework Framework { get; }

    IEnumerable<PackageReference> PackageReferences { get; }
}
