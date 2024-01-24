using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Community.VisualStudio.Toolkit;
using EnvDTE;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.LanguageServices;
using Microsoft.VisualStudio.Shell;
using NetTestX.CodeAnalysis;
using NetTestX.CodeAnalysis.Common;
using NetTestX.CodeAnalysis.Workspaces.Projects;
using NetTestX.VSIX.Extensions;

namespace NetTestX.VSIX.Code;

public class TestSourceCodeCoordinator
{
    public async Task LoadTestSourceCodeAsync(TestSourceCodeLoadingContext context)
    {
        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

        var projectItem = (ProjectItem)context.SelectedItems[0].Object;

        var workspace = await VS.GetMefServiceAsync<VisualStudioWorkspace>();

        var selectedProject = workspace.FindProjectByName(projectItem.ContainingProject.Name);

        CodeProject targetProject = new(context.Project.FileName);
        var driver = await GetGeneratorDriverAsync(context, selectedProject, targetProject, projectItem.FileNames[0]);

        string testSource = await driver.GenerateTestClassSourceAsync();
        string testFileName = $"{Path.GetFileNameWithoutExtension(projectItem.Name)}Tests.{SourceFileExtensions.CSHARP}";

        await AddSourceFileToProjectAsync(context.Project, testSource, testFileName);
    }

    private async Task<UnitTestGeneratorDriver> GetGeneratorDriverAsync(TestSourceCodeLoadingContext context, RoslynProject project, CodeProject targetProject, string fileName)
    {
        var compilation = await project.GetCompilationAsync();

        var syntaxTree = compilation?.SyntaxTrees.FirstOrDefault(x => x.FilePath == fileName);
        var syntaxRoot = await syntaxTree.GetRootAsync();

        var typeDeclaration = syntaxRoot
            .DescendantNodes(node => node is not TypeDeclarationSyntax)
            .OfType<TypeDeclarationSyntax>()
            .First();

        var typeSymbol = compilation.GetSemanticModel(syntaxTree).GetDeclaredSymbol(typeDeclaration);

        UnitTestGeneratorContext generatorContext = new()
        {
            Type = typeSymbol,
            Compilation = compilation,
            Options = context.OptionsProvider.GetOptions(targetProject, typeSymbol)
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
