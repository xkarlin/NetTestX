using System.Linq;
using NetTestX.Common;

namespace NetTestX.CodeAnalysis.Workspaces.Projects.Testing.TestFrameworks;

internal static class TestFrameworkProjectLocator
{
    private static readonly ITestFrameworkProjectDetector[] _detectors =
    [
        new XUnitDetector(),
        new NUnitDetector(),
        new MSTestDetector()
    ];

    public static TestFramework GetTestFramework(CodeProject project) => _detectors.First(x => x.Detect(project)).TestFramework;
}
