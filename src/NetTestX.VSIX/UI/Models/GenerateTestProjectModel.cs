using NetTestX.Common;

namespace NetTestX.VSIX.Models;

public class GenerateTestProjectModel
{
    public string ProjectName { get; set; }

    public string ProjectDirectory { get; set; }

    public TestFramework TestFramework { get; set; }

    public MockingLibrary MockingLibrary { get; set; }
}
