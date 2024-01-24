﻿using System.Collections.Generic;
using NetTestX.Common;

namespace NetTestX.CodeAnalysis.Workspaces.Generation.Testing.MockingLibraries;

public interface IMockingLibraryProjectModel
{
    MockingLibrary Library { get; }

    IEnumerable<PackageReference> PackageReferences { get; }
}