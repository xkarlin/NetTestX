using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell;
using NetTestX.CodeAnalysis.Workspaces.Projects;
using NetTestX.VSIX.UI.Models;
using NetTestX.VSIX.UI.Views;

namespace NetTestX.VSIX.Projects;

public static class TestProjectUtility
{
    private const string INTERNALS_VISIBLE_TO_ITEM_NAME = "InternalsVisibleTo";

    public static async Task<DTEProject> CreateTestProjectFromViewAsync(TestProjectFactoryContext context)
    {
        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

        var testProjectFactory = await TestProjectFactory.CreateAsync(context);

        GenerateTestProjectModel model = new()
        {
            ProjectName = testProjectFactory.Options.ProjectName,
            ProjectDirectory = testProjectFactory.Options.ProjectDirectory,
            TestFramework = testProjectFactory.Options.TestFramework,
            MockingLibrary = testProjectFactory.Options.MockingLibrary,
            GenerateInternalsVisibleTo = false
        };

        GenerateTestProjectView view = new(model);
        bool? result = view.ShowDialog();

        if (result != true)
            return null;

        if (model.GenerateInternalsVisibleTo)
            AddInternalsVisibleTo(context.Project, model.ProjectName);

        testProjectFactory.Options.ProjectName = model.ProjectName;
        testProjectFactory.Options.ProjectDirectory = model.ProjectDirectory;
        testProjectFactory.Options.TestFramework = model.TestFramework;
        testProjectFactory.Options.MockingLibrary = model.MockingLibrary;

        return await testProjectFactory.CreateTestProjectAsync();
    }

    public static void AddInternalsVisibleTo(DTEProject sourceProject, string visibleTo)
    {
        ThreadHelper.ThrowIfNotOnUIThread();

        CodeProject codeProject = new(sourceProject.FileName);
        codeProject.AddItem(INTERNALS_VISIBLE_TO_ITEM_NAME, visibleTo);
        codeProject.Save();
    }
}
