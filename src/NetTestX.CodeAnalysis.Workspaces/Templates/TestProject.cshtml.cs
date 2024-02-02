using NetTestX.CodeAnalysis.Workspaces.Generation.Testing.MockingLibraries;
using NetTestX.CodeAnalysis.Workspaces.Generation.Testing.TestFrameworks;

namespace NetTestX.CodeAnalysis.Workspaces.Templates;

public class TestProjectModel
{
    public string ProjectName { get; set; }

    public string OriginalProjectRelativePath { get; set; }

    public ITestFrameworkProjectModel TestFrameworkModel { get; set; }

    public IMockingLibraryProjectModel MockingLibraryModel { get; set; }
}
