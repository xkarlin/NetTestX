using System.Threading.Tasks;
using Community.VisualStudio.Toolkit;
using Microsoft.VisualStudio.Shell;
using NetTestX.VSIX.Commands.Handlers;

namespace NetTestX.VSIX.Commands;

public abstract class BaseCommand<TCommand, THandler> : BaseCommand<TCommand>
    where TCommand : BaseCommand<TCommand, THandler>, new()
    where THandler : ICommandHandler
{
    protected abstract THandler CreateHandler();

    protected sealed override Task ExecuteAsync(OleMenuCmdEventArgs e)
    {
        var handler = CreateHandler();
        return handler.ExecuteAsync();
    }
}
