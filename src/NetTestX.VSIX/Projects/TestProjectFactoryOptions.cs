using NetTestX.Common;

namespace NetTestX.VSIX.Projects;

/// <summary>
/// Options used by <see cref="TestProjectFactory"/> when creating projects
/// </summary>
public class TestProjectFactoryOptions
{
    /// <summary>
    /// The name of the project
    /// </summary>
    public required string ProjectName { get; set; }

    /// <summary>
    /// The directory of the project
    /// </summary>
    public required string ProjectDirectory { get; set; }

    /// <summary>
    /// The testing framework that the project uses
    /// </summary>
    public required TestFramework TestFramework { get; set; }

    /// <summary>
    /// The mocking library that the project uses
    /// </summary>
    public required MockingLibrary MockingLibrary { get; set; }
}
