using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using NetTestX.VSIX.Extensions;
using NetTestX.VSIX.Projects;
using NetTestX.VSIX.UI.Views;
using System.Threading.Tasks;
using NetTestX.VSIX.UI.Models;
using NetTestX.CodeAnalysis.Workspaces.Projects;

namespace NetTestX.VSIX.Commands.Handlers;

public class GenerateTestProjectCommandHandler(DTE2 dte)
{
    private const string INTERNALS_VISIBLE_TO_ITEM_NAME = "InternalsVisibleTo";

    public async Task ExecuteAsync()
    {
        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

        GenerateTestProjectModel model = new();
        GenerateTestProjectView view = new(model);
        bool? result = view.ShowDialog();

        if (result != true)
            return;

        TestProjectFactoryContext context = new()
        {
            DTE = dte,
            Project = dte.GetSelectedProjectFromSolutionExplorer()
        };

        if (model.GenerateInternalsVisibleTo)
        {
            CodeProject originalProject = new(context.Project.FileName);
            originalProject.AddItem(INTERNALS_VISIBLE_TO_ITEM_NAME, model.ProjectName);
            originalProject.Save();
        }

        TestProjectFactory testProjectFactory = new();
        await testProjectFactory.CreateTestProjectAsync(context, model);
    }
}
