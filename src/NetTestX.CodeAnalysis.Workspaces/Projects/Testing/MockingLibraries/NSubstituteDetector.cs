using System.Linq;
using NetTestX.CodeAnalysis.Workspaces.Extensions;
using NetTestX.Common;

namespace NetTestX.CodeAnalysis.Workspaces.Projects.Testing.MockingLibraries;

public class NSubstituteDetector : IMockingLibraryProjectDetector
{
    public MockingLibrary MockingLibrary => MockingLibrary.NSubstitute;

    public bool Detect(CodeProject project) => project.GetPackageReferences().Any(x => x.Include == "NSubstitute");
}
