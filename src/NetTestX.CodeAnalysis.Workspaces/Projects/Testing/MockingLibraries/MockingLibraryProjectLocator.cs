using NetTestX.Common;
using System.Linq;

namespace NetTestX.CodeAnalysis.Workspaces.Projects.Testing.MockingLibraries;

public static class MockingLibraryProjectLocator
{
    private static readonly IMockingLibraryProjectDetector[] _detectors =
    [
        new NSubstituteDetector(),
        new FakeItEasyDetector()
    ];

    public static MockingLibrary GetMockingLibrary(CodeProject project) => _detectors.First(x => x.Detect(project)).MockingLibrary;
}
