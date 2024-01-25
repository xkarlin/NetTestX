using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Community.VisualStudio.Toolkit;
using EnvDTE;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.LanguageServices;
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

    private readonly Compilation _compilation;
    
    public required TestSourceCodeCoordinatorOptions Options { get; init; }

    private TestSourceCodeCoordinator(INamedTypeSymbol type, Compilation compilation)
    {
        _type = type;
        _compilation = compilation;
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

        var projectItem = (ProjectItem)context.SelectedItems[0].Object;

        var workspace = await VS.GetMefServiceAsync<VisualStudioWorkspace>();
        var sourceProject = workspace.FindProjectByName(projectItem.ContainingProject.Name);

        string sourceFileName = projectItem.FileNames[0];

        var compilation = await sourceProject.GetCompilationAsync();

        var syntaxTree = compilation?.SyntaxTrees.FirstOrDefault(x => x.FilePath == sourceFileName);
        var syntaxRoot = await syntaxTree.GetRootAsync();

        var typeDeclaration = syntaxRoot
            .DescendantNodes(node => node is not TypeDeclarationSyntax)
            .OfType<TypeDeclarationSyntax>()
            .First();

        var typeSymbol = compilation.GetSemanticModel(syntaxTree).GetDeclaredSymbol(typeDeclaration);

        return new(typeSymbol, compilation)
        {
            Options = new()
            {
                TestFileName = $"{Path.GetFileNameWithoutExtension(sourceFileName)}Tests",
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
            Compilation = _compilation,
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
