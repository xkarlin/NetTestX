using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell;
using NetTestX.VSIX.Extensions;
using NetTestX.VSIX.UI.Models;
using NetTestX.VSIX.UI.Views;

namespace NetTestX.VSIX.Projects;

public class TestProjectCoordinator
{
    public async Task<DTEProject> LoadTestProjectAsync(TestProjectCoordinatorContext context)
    {
        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

        if (context.TestProject is { } testProject)
            return context.DTE.Solution.FindSolutionProject(testProject.Name);

        TestProjectFactoryContext factoryContext = new()
        {
            DTE = context.DTE,
            Project = context.CurrentProject
        };

        var testProjectFactory = await TestProjectFactory.CreateAsync(factoryContext);

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
            context.TestProject.AddItem("InternalsVisibleTo", model.ProjectName);
            context.TestProject.Save();
        }

        testProjectFactory.Options.ProjectName = model.ProjectName;
        testProjectFactory.Options.ProjectDirectory = model.ProjectDirectory;
        testProjectFactory.Options.TestFramework = model.TestFramework;
        testProjectFactory.Options.MockingLibrary = model.MockingLibrary;


        return await testProjectFactory.CreateTestProjectAsync();
    }
}
