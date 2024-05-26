using System.Threading.Tasks;
using Community.VisualStudio.Toolkit;
using EnvDTE;
using EnvDTE80;
using Microsoft;
using Microsoft.VisualStudio.Shell;
using NetTestX.VSIX.Commands.Handlers;

namespace NetTestX.VSIX.Commands;

/// <summary>
/// Base class for creating VS commands
/// </summary>
public abstract class BaseCommand<TCommand, THandler> : BaseCommand<TCommand>
    where TCommand : BaseCommand<TCommand, THandler>, new()
    where THandler : ICommandHandler
{
    protected DTE2 DTE { get; private set; }

    protected override async Task InitializeCompletedAsync()
    {
        DTE = await Package.GetServiceAsync(typeof(DTE)) as DTE2;
        Assumes.Present(DTE);
    }

    protected abstract THandler CreateHandler();

    protected sealed override Task ExecuteAsync(OleMenuCmdEventArgs e)
    {
        var handler = CreateHandler();
        return handler.ExecuteAsync();
    }
}
