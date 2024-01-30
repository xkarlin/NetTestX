using System.Threading.Tasks;
using Community.VisualStudio.Toolkit;
using EnvDTE;
using EnvDTE80;
using Microsoft;
using Microsoft.VisualStudio.Shell;
using NetTestX.VSIX.Commands.Handlers;

namespace NetTestX.VSIX.Commands;

public abstract class BaseDynamicCommand<TCommand, THandler, TItem> : BaseDynamicCommand<TCommand, TItem>
    where TCommand : BaseDynamicCommand<TCommand, THandler, TItem>, new()
    where THandler : ICommandHandler
{
    protected DTE2 DTE { get; private set; }

    protected override async Task InitializeCompletedAsync()
    {
        DTE = await Package.GetServiceAsync(typeof(DTE)) as DTE2;
        Assumes.Present(DTE);
    }

    protected abstract THandler CreateHandler(TItem item);

    protected sealed override Task ExecuteAsync(OleMenuCmdEventArgs e, TItem item)
    {
        var handler = CreateHandler(item);
        return handler.ExecuteAsync();
    }
}
