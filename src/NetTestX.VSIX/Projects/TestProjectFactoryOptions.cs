using NetTestX.Common;

namespace NetTestX.VSIX.Projects;

public class TestProjectFactoryOptions
{
    public required string ProjectName { get; set; }

    public required string ProjectDirectory { get; set; }

    public required TestFramework TestFramework { get; set; }

    public required MockingLibrary MockingLibrary { get; set; }
}
