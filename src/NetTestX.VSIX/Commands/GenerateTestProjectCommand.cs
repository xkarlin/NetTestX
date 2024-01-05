using Community.VisualStudio.Toolkit;
using EnvDTE;
using EnvDTE80;
using Microsoft;
using Microsoft.VisualStudio.Shell;
using NetTestX.VSIX.Extensions;
using NetTestX.VSIX.Models;
using NetTestX.VSIX.Projects;
using NetTestX.VSIX.UI.Views;
using System.Threading.Tasks;

namespace NetTestX.VSIX.Commands;

[Command(PackageIds.GenerateTestProjectCommand)]
internal class GenerateTestProjectCommand : BaseCommand<GenerateTestProjectCommand>
{
    private DTE2 _dte;

    protected override async Task InitializeCompletedAsync()
    {
        _dte = await Package.GetServiceAsync(typeof(DTE)) as DTE2;
        Assumes.Present(_dte);
    }

    protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
    {
        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

        GenerateTestProjectModel model = new();
        GenerateTestProjectView view = new(model);
        bool? result = view.ShowDialog();

        if (result != true)
            return;

        TestProjectLoadingContext context = new()
        {
            DTE = _dte,
            Project = _dte.GetSelectedProjectFromSolutionExplorer()
        };

        TestProjectFactory testProjectFactory = new();
        await testProjectFactory.CreateTestProjectAsync(context, model);
    }
}
