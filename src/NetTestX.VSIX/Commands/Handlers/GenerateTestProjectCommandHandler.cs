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

        TestProjectFactoryContext context = new()
        {
            DTE = dte,
            Project = dte.GetSelectedProjectFromSolutionExplorer()
        };

        var testProjectFactory = await TestProjectFactory.CreateAsync(context);

        GenerateTestProjectModel model = new()
        {
            ProjectName = testProjectFactory.Options.ProjectName,
            ProjectDirectory = testProjectFactory.Options.ProjectDirectory,
            TestFramework = testProjectFactory.Options.TestFramework,
            MockingLibrary = testProjectFactory.Options.MockingLibrary,
            GenerateInternalsVisibleTo = true
        };

        GenerateTestProjectView view = new(model);
        bool? result = view.ShowDialog();

        if (result != true)
            return;

        if (model.GenerateInternalsVisibleTo)
        {
            CodeProject originalProject = new(context.Project.FileName);
            originalProject.AddItem(INTERNALS_VISIBLE_TO_ITEM_NAME, model.ProjectName);
            originalProject.Save();
        }

        testProjectFactory.Options.ProjectName = model.ProjectName;
        testProjectFactory.Options.ProjectDirectory = model.ProjectDirectory;
        testProjectFactory.Options.TestFramework = model.TestFramework;
        testProjectFactory.Options.MockingLibrary = model.MockingLibrary;

        await testProjectFactory.CreateTestProjectAsync();
    }
}
