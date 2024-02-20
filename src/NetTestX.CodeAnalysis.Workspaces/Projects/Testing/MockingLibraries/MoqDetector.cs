using NetTestX.CodeAnalysis.Workspaces.Extensions;
using NetTestX.Common;
using System.Linq;

namespace NetTestX.CodeAnalysis.Workspaces.Projects.Testing.MockingLibraries;

internal class MoqDetector : IMockingLibraryProjectDetector
{
    public MockingLibrary MockingLibrary => MockingLibrary.Moq;

    public bool Detect(CodeProject project) => project.GetPackageReferences().Any(x => x.Include == "Moq");
}
