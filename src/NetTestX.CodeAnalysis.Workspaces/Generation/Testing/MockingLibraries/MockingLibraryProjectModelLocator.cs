using System;
using NetTestX.Common;

namespace NetTestX.CodeAnalysis.Workspaces.Generation.Testing.MockingLibraries;

/// <summary>
/// Helper class used to locate instances of <see cref="IMockingLibraryProjectModel"/> for <see cref="MockingLibrary"/>
/// </summary>
public static class MockingLibraryProjectModelLocator
{
    /// <summary>
    /// Locate an instance of <see cref="IMockingLibraryProjectModel"/> for the given <paramref name="library"/>
    /// </summary>
    public static IMockingLibraryProjectModel LocateModel(MockingLibrary library) => library switch
    {
        MockingLibrary.NSubstitute => new NSubstituteProjectModel(),
        MockingLibrary.FakeItEasy => new FakeItEasyProjectModel(),
        MockingLibrary.Moq => new MoqProjectModel(),
        _ => throw new NotSupportedException()
    };
}
