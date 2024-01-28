using System.Threading.Tasks;
using Community.VisualStudio.Toolkit;
using EnvDTE;
using EnvDTE80;
using Microsoft;
using NetTestX.VSIX.Commands.Handlers;

namespace NetTestX.VSIX.Commands;

[Command(PackageIds.GenerateTestProjectCommand)]
internal class GenerateTestProjectCommand : BaseCommand<GenerateTestProjectCommand, GenerateTestProjectCommandHandler>
{
    private DTE2 _dte;

    protected override async Task InitializeCompletedAsync()
    {
        _dte = await Package.GetServiceAsync(typeof(DTE)) as DTE2;
        Assumes.Present(_dte);
    }

    protected override GenerateTestProjectCommandHandler CreateHandler() => new(_dte);
}
