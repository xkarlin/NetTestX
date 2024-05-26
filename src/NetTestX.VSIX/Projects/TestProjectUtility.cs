using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell;
using NetTestX.CodeAnalysis.Workspaces.Projects;
using NetTestX.VSIX.UI.Models;
using NetTestX.VSIX.UI.Views;

namespace NetTestX.VSIX.Projects;

/// <summary>
/// Helper class used to generate test projects
/// </summary>
public static class TestProjectUtility
{
    private const string INTERNALS_VISIBLE_TO_ITEM_NAME = "InternalsVisibleTo";

    /// <summary>
    /// Generate a test <see cref="DTEProject"/> from the project creation view (<see cref="GenerateTestProjectView"/>)
    /// </summary>
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

    /// <summary>
    /// Add the InternalsVisibleTo <paramref name="visibleTo"/> item to the provided <paramref name="sourceProject"/>
    /// </summary>
    public static void AddInternalsVisibleTo(DTEProject sourceProject, string visibleTo)
    {
        ThreadHelper.ThrowIfNotOnUIThread();

        CodeProject codeProject = new(sourceProject.FileName);

        if (codeProject.GetItems(INTERNALS_VISIBLE_TO_ITEM_NAME).Any(x => x.Include == visibleTo))
            return;
        
        codeProject.AddItem(INTERNALS_VISIBLE_TO_ITEM_NAME, visibleTo);
        codeProject.Save();
    }
}
