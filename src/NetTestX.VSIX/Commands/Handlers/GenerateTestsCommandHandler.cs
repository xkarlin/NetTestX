using System.Linq;
using System.Threading.Tasks;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using NetTestX.CodeAnalysis.Workspaces.Projects;
using NetTestX.VSIX.Code;
using NetTestX.VSIX.Code.TypeSymbolProviders;
using NetTestX.VSIX.Extensions;
using NetTestX.VSIX.Projects;

namespace NetTestX.VSIX.Commands.Handlers;

public class GenerateTestsCommandHandler(DTE2 dte, CodeProject testProject) : ICommandHandler
{
    public async Task ExecuteAsync()
    {
        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

        var selectedItems = dte.GetSelectedItemsFromSolutionExplorer();

        var projectItem = (ProjectItem)selectedItems[0].Object;
        var sourceProject = await projectItem.FindRoslynProjectAsync();

        string sourceFileName = projectItem.FileNames[0];
        var compilation = await sourceProject.GetCompilationAsync();
        var syntaxTree = compilation?.SyntaxTrees.FirstOrDefault(x => x.FilePath == sourceFileName);

        var targetProject = await GetTargetProjectAsync(selectedItems);

        TestSourceCodeLoadingContext sourceCodeLoadingContext = new()
        {
            DTE = dte,
            TypeSymbolProvider = new SyntaxTreeTypeSymbolProvider(compilation, syntaxTree)
        };

        var codeCoordinator = await TestSourceCodeCoordinator.CreateAsync(sourceCodeLoadingContext);

        await codeCoordinator.LoadSourceCodeAsync(targetProject);
    }

    private async Task<DTEProject> GetTargetProjectAsync(UIHierarchyItem[] selectedItems)
    {
        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

        if (testProject is not null)
            return dte.Solution.FindSolutionProject(testProject.Name);

        TestProjectFactoryContext context = new()
        {
            DTE = dte,
            Project = ((ProjectItem)selectedItems[0].Object).ContainingProject
        };

        return await TestProjectUtility.CreateTestProjectFromViewAsync(context);
    }
}
