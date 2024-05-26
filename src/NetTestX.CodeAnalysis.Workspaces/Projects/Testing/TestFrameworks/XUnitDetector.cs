using System.Linq;
using NetTestX.CodeAnalysis.Workspaces.Extensions;
using NetTestX.Common;

namespace NetTestX.CodeAnalysis.Workspaces.Projects.Testing.TestFrameworks;

internal class XUnitDetector : ITestFrameworkProjectDetector
{
    public TestFramework TestFramework => TestFramework.XUnit;

    public bool Detect(CodeProject project) => project.GetPackageReferences().Any(x => x.Include == "xunit");
}
