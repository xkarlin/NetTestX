using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.Shell;
using NetTestX.CodeAnalysis;
using NetTestX.CodeAnalysis.Common;
using NetTestX.CodeAnalysis.Workspaces.Extensions;
using NetTestX.CodeAnalysis.Workspaces.Projects;
using NetTestX.Common.Diagnostics;
using NetTestX.VSIX.Extensions;

namespace NetTestX.VSIX.Code;

public class TestSourceCodeCoordinator
{
    public required TestSourceCodeCoordinatorOptions Options { get; init; }

    public required UnitTestGeneratorDriver.Builder DriverBuilder { get; init; }

    public async Task LoadSourceCodeAsync(DTEProject targetProject)
    {
        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

        CodeProject codeProject = new(targetProject.FileName);

        var driver = GetGeneratorDriver(codeProject);

        string testSource = await driver.GenerateTestClassSourceAsync();
        string testSourceFileName = $"{Options.TestFileName}.{SourceFileExtensions.CSHARP}";
        
        await AddSourceFileToProjectAsync(targetProject, testSource, testSourceFileName);
    }

    public static async Task<TestSourceCodeCoordinator> CreateAsync(INamedTypeSymbol type, DTEProject sourceProject, IDiagnosticReporter reporter = null)
    {
        RoslynProject roslynProject = await sourceProject.FindRoslynProjectAsync();
        Compilation compilation = await roslynProject.GetCompilationAsync();

        TestSourceCodeCoordinator coordinator = new()
        {
            Options = new()
            {
                TestFileName = $"{type.Name}Tests"
            },
            DriverBuilder = UnitTestGeneratorDriver.CreateBuilder(type, compilation, reporter)
        };

        coordinator.DriverBuilder.TestClassName = $"{type.Name}Tests";
        coordinator.DriverBuilder.TestClassNamespace = $"{type.ContainingNamespace}.Tests";

        return coordinator;
    }

    private UnitTestGeneratorDriver GetGeneratorDriver(CodeProject project)
    {
        DriverBuilder.TestFramework = project.GetProjectTestFramework();
        DriverBuilder.MockingLibrary = project.GetProjectMockingLibrary();

        UnitTestGeneratorDriver driver = DriverBuilder.Build();
        return driver;
    }

    private async Task AddSourceFileToProjectAsync(DTEProject project, string sourceText, string fileName)
    {
        MemoryStream ms = new(Encoding.UTF8.GetBytes(sourceText));
        await project.CreateProjectFileAsync(ms, fileName);
    }
}
