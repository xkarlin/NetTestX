using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using NetTestX.CodeAnalysis.Workspaces;
using NetTestX.CodeAnalysis.Workspaces.Extensions;
using NetTestX.VSIX.UI.Views;
using System.Linq;
using System.Threading.Tasks;
using NetTestX.VSIX.Code;
using NetTestX.VSIX.Extensions;
using NetTestX.VSIX.UI.Models;

namespace NetTestX.VSIX.Commands.Handlers;

internal class GenerateTestsAdvancedCommandHandler(DTE2 dte)
{
    public async Task ExecuteAsync()
    {
        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

        TestSourceCodeLoadingContext codeLoadingContext = new()
        {
            DTE = dte,
            SelectedItems = dte.GetSelectedItemsFromSolutionExplorer(),
        };

        var codeCoordinator = await TestSourceCodeCoordinator.CreateAsync(codeLoadingContext);

        GenerateTestsAdvancedModel model = new()
        {
            TestFileName = codeCoordinator.Options.TestFileName,
            TestClassName = codeCoordinator.Options.TestClassName,
            TestClassNamespace = codeCoordinator.Options.TestClassNamespace
        };

        var workspace = CodeWorkspace.Open(dte.Solution.FileName);
        var testProjects = workspace.GetTestProjects();

        GenerateTestsAdvancedView view = new(model, testProjects.Select(x => x.Name));

        bool? result = view.ShowDialog();
        
        if (result != true)
            return;

        codeCoordinator.Options.TestFileName = model.TestFileName;
        codeCoordinator.Options.TestClassName = model.TestClassName;
        codeCoordinator.Options.TestClassNamespace = model.TestClassNamespace;

        var targetProject = dte.Solution.FindSolutionProject(model.TestProject);

        await codeCoordinator.LoadSourceCodeAsync(targetProject);
    }
}
