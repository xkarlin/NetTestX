using System.Collections.Generic;
using NetTestX.Common;

namespace NetTestX.CodeAnalysis.Workspaces.Projects.Testing.MockingLibraries;

public class NSubstituteProjectModel : IMockingLibraryProjectModel
{
    public MockingLibrary Library => MockingLibrary.NSubstitute;

    public IEnumerable<PackageReference> PackageReferences => [new("NSubstitute")];
}
