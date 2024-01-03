using NetTestX.Common;

namespace NetTestX.CodeAnalysis.Workspaces.Projects.Testing;

public readonly struct TestProjectCreationContext
{
    public required string ProjectName { get; init; }
    
    public required string ProjectFilePath { get; init; }
    
    public required string OriginalProjectPath { get; init; }

    public required MockingLibrary MockingLibrary { get; init; }

    public required TestFramework TestFramework { get; init; }
}
