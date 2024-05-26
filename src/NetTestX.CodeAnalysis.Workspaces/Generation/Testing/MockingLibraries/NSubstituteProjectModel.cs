using System.Collections.Generic;
using NetTestX.Common;

namespace NetTestX.CodeAnalysis.Workspaces.Generation.Testing.MockingLibraries;

/// <summary>
/// <see cref="IMockingLibraryProjectModel"/> for NSubstitute
/// </summary>
public class NSubstituteProjectModel : IMockingLibraryProjectModel
{
    public MockingLibrary Library => MockingLibrary.NSubstitute;

    public IEnumerable<PackageReference> PackageReferences { get; } = [new("NSubstitute")];
}
