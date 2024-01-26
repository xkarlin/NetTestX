using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell;
using NetTestX.CodeAnalysis.Workspaces.Projects;
using NetTestX.VSIX.UI.Models;
using NetTestX.VSIX.UI.Views;

namespace NetTestX.VSIX.Projects;

public static class TestProjectUtility
{
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
            GenerateInternalsVisibleTo = true
        };

        GenerateTestProjectView view = new(model);
        bool? result = view.ShowDialog();

        if (result != true)
            return null;

        if (model.GenerateInternalsVisibleTo)
        {
            CodeProject originalProject = new(context.Project.FileName);
            originalProject.AddItem("InternalsVisibleTo", model.ProjectName);
            originalProject.Save();
        }

        testProjectFactory.Options.ProjectName = model.ProjectName;
        testProjectFactory.Options.ProjectDirectory = model.ProjectDirectory;
        testProjectFactory.Options.TestFramework = model.TestFramework;
        testProjectFactory.Options.MockingLibrary = model.MockingLibrary;

        return await testProjectFactory.CreateTestProjectAsync();
    }
}
