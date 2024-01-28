using System.Threading.Tasks;
using Community.VisualStudio.Toolkit;
using Microsoft.VisualStudio.Shell;
using NetTestX.VSIX.Commands.Handlers;

namespace NetTestX.VSIX.Commands;

public abstract class BaseDynamicCommand<TCommand, THandler, TItem> : BaseDynamicCommand<TCommand, TItem>
    where TCommand : BaseDynamicCommand<TCommand, THandler, TItem>, new()
    where THandler : ICommandHandler
{
    protected abstract THandler CreateHandler(TItem item);

    protected sealed override Task ExecuteAsync(OleMenuCmdEventArgs e, TItem item)
    {
        var handler = CreateHandler(item);
        return handler.ExecuteAsync();
    }
}
