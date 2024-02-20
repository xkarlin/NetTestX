using NetTestX.Common;
using System;
using System.Collections.Generic;

namespace NetTestX.CodeAnalysis.Workspaces.Generation.Testing.MockingLibraries;

public class MoqProjectModel : IMockingLibraryProjectModel
{
    public MockingLibrary Library => MockingLibrary.Moq;

    public IEnumerable<PackageReference> PackageReferences { get; } = [new("Moq")];
}
