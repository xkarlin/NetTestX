using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using NetTestX.CodeAnalysis.Workspaces;
using NetTestX.CodeAnalysis.Workspaces.Extensions;
using NetTestX.VSIX.UI.Views;
using System;
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

        var workspace = CodeWorkspace.Open(dte.Solution.FileName);
        var testProjects = workspace.GetTestProjects();

        GenerateTestsAdvancedModel model = new();
        GenerateTestsAdvancedView view = new(model, testProjects.Select(x => x.Name));

        bool? result = view.ShowDialog();
        
        if (result != true)
            return;

        TestSourceCodeCoordinator codeCoordinator = new();
        TestSourceCodeLoadingContext codeLoadingContext = new()
        {
            DTE = dte,
            Project = dte.Solution.FindSolutionProject(model.TestProject),
            SelectedItems = dte.GetSelectedItemsFromSolutionExplorer(),
            OptionsProvider = new CustomTestGeneratorOptionsProvider(model.TestClassName, model.TestClassNamespace)
        };

        await codeCoordinator.LoadTestSourceCodeAsync(codeLoadingContext);
    }
}
