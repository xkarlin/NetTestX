using EnvDTE;
using EnvDTE80;

namespace NetTestX.VSIX.Projects;

public readonly struct TestProjectLoadingContext
{
    public required DTE2 DTE { get; init; }

    public required Project Project { get; init; }
}
