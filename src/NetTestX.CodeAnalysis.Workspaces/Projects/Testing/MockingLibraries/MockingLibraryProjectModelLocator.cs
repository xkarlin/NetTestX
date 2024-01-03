﻿using System;
using NetTestX.Common;

namespace NetTestX.CodeAnalysis.Workspaces.Projects.Testing.MockingLibraries;

public static class MockingLibraryProjectModelLocator
{
    public static IMockingLibraryProjectModel LocateModel(MockingLibrary library) => library switch
    {
        MockingLibrary.NSubstitute => new NSubstituteProjectModel(),
        _ => throw new NotSupportedException()
    };
}