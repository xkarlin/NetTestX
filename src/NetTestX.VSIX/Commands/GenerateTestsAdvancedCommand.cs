using Community.VisualStudio.Toolkit;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft;
using NetTestX.VSIX.Commands.Handlers;
using System.Threading.Tasks;

namespace NetTestX.VSIX.Commands;

[Command(PackageIds.GenerateTestsAdvancedCommand)]
internal sealed class GenerateTestsAdvancedCommand : BaseCommand<GenerateTestsAdvancedCommand>
{
    private DTE2 _dte;

    protected override async Task InitializeCompletedAsync()
    {
        _dte = await Package.GetServiceAsync(typeof(DTE)) as DTE2;
        Assumes.Present(_dte);
    }

    protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
    {
        GenerateTestsAdvancedCommandHandler handler = new(_dte);
        await handler.ExecuteAsync();
    }
}
