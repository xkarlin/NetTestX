using System.Collections.Generic;
using NetTestX.Common;

namespace NetTestX.CodeAnalysis.Workspaces.Generation.Testing.MockingLibraries;

/// <summary>
/// <see cref="IMockingLibraryProjectModel"/> for FakeItEasy
/// </summary>
public class FakeItEasyProjectModel : IMockingLibraryProjectModel
{
    public MockingLibrary Library => MockingLibrary.FakeItEasy;

    public IEnumerable<PackageReference> PackageReferences { get; } = [new("FakeItEasy")];
}
