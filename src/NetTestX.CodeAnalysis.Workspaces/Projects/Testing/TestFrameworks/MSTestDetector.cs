using System.Linq;
using NetTestX.CodeAnalysis.Workspaces.Extensions;
using NetTestX.Common;

namespace NetTestX.CodeAnalysis.Workspaces.Projects.Testing.TestFrameworks;

internal class MSTestDetector : ITestFrameworkProjectDetector
{
    public TestFramework TestFramework => TestFramework.MSTest;

    public bool Detect(CodeProject project) => project.GetPackageReferences().Any(x => x.Include == "MSTest.TestAdapter");
}
