using NetTestX.CodeAnalysis.Workspaces.Extensions;
using NetTestX.Common;
using System.Linq;

namespace NetTestX.CodeAnalysis.Workspaces.Projects.Testing.TestFrameworks;

internal class NUnitDetector : ITestFrameworkProjectDetector
{
    public TestFramework TestFramework => TestFramework.NUnit;

    public bool Detect(CodeProject project) => project.GetPackageReferences().Any(x => x.Include == "NUnit");
}
