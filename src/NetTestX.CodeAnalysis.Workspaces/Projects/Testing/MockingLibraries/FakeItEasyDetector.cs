using System.Linq;
using NetTestX.CodeAnalysis.Workspaces.Extensions;
using NetTestX.Common;

namespace NetTestX.CodeAnalysis.Workspaces.Projects.Testing.MockingLibraries;

internal class FakeItEasyDetector : IMockingLibraryProjectDetector
{
    public MockingLibrary MockingLibrary => MockingLibrary.FakeItEasy;

    public bool Detect(CodeProject project) => project.GetPackageReferences().Any(x => x.Include == "FakeItEasy");
}
