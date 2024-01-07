using EnvDTE;
using EnvDTE80;

namespace NetTestX.VSIX.Projects;

public readonly struct TestProjectFactoryContext
{
    public required DTE2 DTE { get; init; }

    public required Project Project { get; init; }
}
