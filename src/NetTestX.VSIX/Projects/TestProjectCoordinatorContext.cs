using EnvDTE;
using EnvDTE80;
using NetTestX.CodeAnalysis.Workspaces.Projects;

namespace NetTestX.VSIX.Projects;

public readonly struct TestProjectCoordinatorContext
{
    public required DTE2 DTE { get; init; }

    public required Project CurrentProject { get; init; }

    public required CodeProject TestProject { get; init; }
}
