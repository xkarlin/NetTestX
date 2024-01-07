using Community.VisualStudio.Toolkit;
using EnvDTE;
using EnvDTE80;
using Microsoft;
using Microsoft.VisualStudio.Shell;
using NetTestX.VSIX.Commands.Handlers;
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
        GenerateTestProjectCommandHandler handler = new(_dte);
        await handler.ExecuteAsync();
    }
}
