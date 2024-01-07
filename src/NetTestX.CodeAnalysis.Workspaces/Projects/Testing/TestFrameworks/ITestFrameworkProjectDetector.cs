using NetTestX.Common;

namespace NetTestX.CodeAnalysis.Workspaces.Projects.Testing.TestFrameworks;

internal interface ITestFrameworkProjectDetector
{
    TestFramework TestFramework { get; }

    bool Detect(CodeProject project);
}
