using NetTestX.Common;

namespace NetTestX.CodeAnalysis.Workspaces.Projects.Testing;

/// <summary>
/// Context used for creating testing <see cref="CodeProject"/>s
/// </summary>
public readonly struct TestProjectCreationContext
{
    /// <summary>
    /// The name of the testing project
    /// </summary>
    public required string ProjectName { get; init; }
    
    /// <summary>
    /// The file path where the project file should be created
    /// </summary>
    public required string ProjectFilePath { get; init; }
    
    /// <summary>
    /// The path where the original project (for which the testing project is being created) is located
    /// </summary>
    public required string OriginalProjectPath { get; init; }

    /// <summary>
    /// The mocking library used by the testing project
    /// </summary>
    public required MockingLibrary MockingLibrary { get; init; }

    /// <summary>
    /// The testing framework used by the testing project
    /// </summary>
    public required TestFramework TestFramework { get; init; }
}
