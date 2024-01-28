using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.Shell;
using NetTestX.CodeAnalysis;
using NetTestX.CodeAnalysis.Common;
using NetTestX.CodeAnalysis.Workspaces.Extensions;
using NetTestX.CodeAnalysis.Workspaces.Projects;
using NetTestX.VSIX.Extensions;

namespace NetTestX.VSIX.Code;

public class TestSourceCodeCoordinator
{
    private readonly INamedTypeSymbol _type;

    public required TestSourceCodeCoordinatorOptions Options { get; init; }

    private TestSourceCodeCoordinator(INamedTypeSymbol type)
    {
        _type = type;
    }

    public async Task LoadSourceCodeAsync(DTEProject project)
    {
        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

        CodeProject codeProject = new(project.FileName);

        var driver = GetGeneratorDriver(codeProject);

        string testSource = await driver.GenerateTestClassSourceAsync();
        string testSourceFileName = $"{Options.TestFileName}.{SourceFileExtensions.CSHARP}";
        
        await AddSourceFileToProjectAsync(project, testSource, testSourceFileName);
    }

    public static async Task<TestSourceCodeCoordinator> CreateAsync(TestSourceCodeLoadingContext context)
    {
        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

        var typeSymbol = await context.TypeSymbolProvider.GetTypeSymbolAsync();

        return new(typeSymbol)
        {
            Options = new()
            {
                TestFileName = $"{typeSymbol.Name}Tests",
                TestClassName = $"{typeSymbol.Name}Tests",
                TestClassNamespace = $"{typeSymbol.ContainingNamespace}.Tests"
            }
        };
    }

    private UnitTestGeneratorDriver GetGeneratorDriver(CodeProject project)
    {
        UnitTestGeneratorContext generatorContext = new()
        {
            Type = _type,
            Options = new()
            {
                TestClassName = Options.TestClassName,
                TestClassNamespace = Options.TestClassNamespace,
                TestFramework = project.GetProjectTestFramework(),
                MockingLibrary = project.GetProjectMockingLibrary()
            }
        };

        UnitTestGeneratorDriver driver = new(generatorContext);
        return driver;
    }

    private async Task AddSourceFileToProjectAsync(DTEProject project, string sourceText, string fileName)
    {
        MemoryStream ms = new(Encoding.UTF8.GetBytes(sourceText));
        await project.CreateProjectFileAsync(ms, fileName);
    }
}
