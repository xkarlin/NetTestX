using NetTestX.CodeAnalysis.Workspaces.Projects.Testing.MockingLibraries;
using NetTestX.CodeAnalysis.Workspaces.Projects.Testing.TestFrameworks;

namespace NetTestX.CodeAnalysis.Workspaces.Templates.Testing;

public class TestProjectModel
{
    public string ProjectName { get; set; }

    public string OriginalProjectRelativePath { get; set; }

    public ITestFrameworkProjectModel TestFrameworkModel { get; set; }

    public IMockingLibraryProjectModel MockingLibraryModel { get; set; }
}
