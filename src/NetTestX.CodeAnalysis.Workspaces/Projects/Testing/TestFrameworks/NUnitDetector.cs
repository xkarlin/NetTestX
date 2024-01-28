using System.Linq;
using NetTestX.CodeAnalysis.Workspaces.Extensions;
using NetTestX.Common;

namespace NetTestX.CodeAnalysis.Workspaces.Projects.Testing.TestFrameworks;

internal class NUnitDetector : ITestFrameworkProjectDetector
{
    public TestFramework TestFramework => TestFramework.NUnit;

    public bool Detect(CodeProject project) => project.GetPackageReferences().Any(x => x.Include == "NUnit");
}
