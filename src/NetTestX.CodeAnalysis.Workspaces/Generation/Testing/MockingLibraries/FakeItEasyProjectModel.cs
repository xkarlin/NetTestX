using System.Collections.Generic;
using NetTestX.Common;

namespace NetTestX.CodeAnalysis.Workspaces.Generation.Testing.MockingLibraries;

internal class FakeItEasyProjectModel : IMockingLibraryProjectModel
{
    public MockingLibrary Library => MockingLibrary.FakeItEasy;

    public IEnumerable<PackageReference> PackageReferences { get; } = [new("FakeItEasy")];
}
