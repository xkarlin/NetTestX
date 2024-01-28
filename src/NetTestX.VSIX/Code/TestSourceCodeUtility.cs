using System.Linq;
using System.Threading.Tasks;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using NetTestX.CodeAnalysis.Workspaces;
using NetTestX.CodeAnalysis.Workspaces.Extensions;
using NetTestX.VSIX.Extensions;
using NetTestX.VSIX.UI.Models;
using NetTestX.VSIX.UI.Views;

namespace NetTestX.VSIX.Code;

public static class TestSourceCodeUtility
{
    public static async Task LoadSourceCodeFromAdvancedViewAsync(DTE2 dte, TestSourceCodeLoadingContext context)
    {
        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

        var codeCoordinator = await TestSourceCodeCoordinator.CreateAsync(context);

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
