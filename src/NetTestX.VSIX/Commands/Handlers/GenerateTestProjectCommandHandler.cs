using EnvDTE80;
using NetTestX.VSIX.Extensions;
using NetTestX.VSIX.Models;
using NetTestX.VSIX.Projects;
using NetTestX.VSIX.UI.Views;
using System.Threading.Tasks;

namespace NetTestX.VSIX.Commands.Handlers;

public class GenerateTestProjectCommandHandler(DTE2 dte)
{
    public async Task ExecuteAsync()
    {
        GenerateTestProjectModel model = new();
        GenerateTestProjectView view = new(model);
        bool? result = view.ShowDialog();

        if (result != true)
            return;

        TestProjectLoadingContext context = new()
        {
            DTE = dte,
            Project = dte.GetSelectedProjectFromSolutionExplorer()
        };

        TestProjectFactory testProjectFactory = new();
        await testProjectFactory.CreateTestProjectAsync(context, model);
    }
}
