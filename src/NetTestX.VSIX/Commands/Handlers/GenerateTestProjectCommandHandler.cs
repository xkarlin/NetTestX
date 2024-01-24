using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using NetTestX.VSIX.Extensions;
using NetTestX.VSIX.Projects;
using NetTestX.VSIX.UI.Views;
using System.Threading.Tasks;
using NetTestX.VSIX.UI.Models;

namespace NetTestX.VSIX.Commands.Handlers;

public class GenerateTestProjectCommandHandler(DTE2 dte)
{
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

        TestProjectFactory testProjectFactory = new();
        await testProjectFactory.CreateTestProjectAsync(context, model);
    }
}
