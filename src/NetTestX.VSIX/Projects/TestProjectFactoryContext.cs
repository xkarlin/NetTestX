using EnvDTE80;
using System.Threading.Tasks;
using System;

namespace NetTestX.VSIX.Projects;

/// <summary>
/// Context passed to <see cref="TestProjectFactory"/>
/// </summary>
public readonly struct TestProjectFactoryContext
{
    /// <summary>
    /// The active <see cref="DTE2"/> instance
    /// </summary>
    public required DTE2 DTE { get; init; }

    /// <summary>
    /// The source project that needs a test project generated for it
    /// </summary>
    public required DTEProject Project { get; init; }

    /// <summary>
    /// Save callback invoked when the project is created
    /// </summary>
    public Func<DTEProject, Task> SaveCallback { get; init; }
}
