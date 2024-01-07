using NetTestX.Common;
using System.Collections.Generic;

namespace NetTestX.CodeAnalysis.Workspaces.Generation.Testing.MockingLibraries;

internal class FakeItEasyProjectModel : IMockingLibraryProjectModel
{
    public MockingLibrary Library => MockingLibrary.FakeItEasy;

    public IEnumerable<PackageReference> PackageReferences { get; } =
    [
        new("FakeItEasy")
    ];
}
