using NetTestX.Common;
using System.Linq;

namespace NetTestX.CodeAnalysis.Workspaces.Projects.Testing.TestFrameworks;

internal static class TestFrameworkProjectLocator
{
    private static readonly ITestFrameworkProjectDetector[] _detectors =
    [
        new XUnitDetector(),
        new NUnitDetector()
    ];

    public static TestFramework GetTestFramework(CodeProject project) => _detectors.First(x => x.Detect(project)).TestFramework;
}
