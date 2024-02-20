using System;
using NetTestX.Common;

namespace NetTestX.CodeAnalysis.Workspaces.Generation.Testing.MockingLibraries;

public static class MockingLibraryProjectModelLocator
{
    public static IMockingLibraryProjectModel LocateModel(MockingLibrary library) => library switch
    {
        MockingLibrary.NSubstitute => new NSubstituteProjectModel(),
        MockingLibrary.FakeItEasy => new FakeItEasyProjectModel(),
        MockingLibrary.Moq => new MoqProjectModel(),
        _ => throw new NotSupportedException()
    };
}
