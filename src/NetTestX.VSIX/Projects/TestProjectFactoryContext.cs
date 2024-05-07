using EnvDTE80;
using System.Threading.Tasks;
using System;

namespace NetTestX.VSIX.Projects;

public readonly struct TestProjectFactoryContext
{
    public required DTE2 DTE { get; init; }

    public required DTEProject Project { get; init; }

    public Func<DTEProject, Task> SaveCallback { get; init; }
}
