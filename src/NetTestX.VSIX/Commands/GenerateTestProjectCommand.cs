using Community.VisualStudio.Toolkit;
using NetTestX.VSIX.Commands.Handlers;

namespace NetTestX.VSIX.Commands;

/// <summary>
/// Command that generates testing projects, uses <see cref="GenerateTestProjectCommandHandler"/>
/// </summary>
[Command(PackageIds.GenerateTestProjectCommand)]
internal class GenerateTestProjectCommand : BaseCommand<GenerateTestProjectCommand, GenerateTestProjectCommandHandler>
{
    protected override GenerateTestProjectCommandHandler CreateHandler() => new(DTE);
}
