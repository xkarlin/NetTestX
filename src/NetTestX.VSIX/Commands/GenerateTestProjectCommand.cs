using Community.VisualStudio.Toolkit;
using NetTestX.VSIX.Commands.Handlers;

namespace NetTestX.VSIX.Commands;

[Command(PackageIds.GenerateTestProjectCommand)]
internal class GenerateTestProjectCommand : BaseCommand<GenerateTestProjectCommand, GenerateTestProjectCommandHandler>
{
    protected override GenerateTestProjectCommandHandler CreateHandler() => new(DTE);
}
