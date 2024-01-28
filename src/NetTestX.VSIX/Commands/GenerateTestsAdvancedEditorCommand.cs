using System;
using Community.VisualStudio.Toolkit;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using NetTestX.VSIX.Commands.Handlers;
using NetTestX.VSIX.Extensions;

namespace NetTestX.VSIX.Commands;

[Command(PackageIds.GenerateTestsAdvancedEditorCommand)]
internal sealed class GenerateTestsAdvancedEditorCommand : BaseCommand<GenerateTestsAdvancedEditorCommand>
{
    private DTE2 _dte;

    private INamedTypeSymbol _activeTypeSymbol;

    protected override async Task InitializeCompletedAsync()
    {
        _dte = await Package.GetServiceAsync(typeof(DTE)) as DTE2;
        Assumes.Present(_dte);
    }

    protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
    {
        GenerateTestsAdvancedEditorCommandHandler handler = new(_dte, _activeTypeSymbol);
        await handler.ExecuteAsync();
    }

    protected override void BeforeQueryStatus(EventArgs e)
    {
        ThreadHelper.ThrowIfNotOnUIThread();

        bool visible = ShouldCommandBeVisible();

        Command.Visible = Command.Enabled = visible;
    }

    private bool ShouldCommandBeVisible()
    {
        ThreadHelper.ThrowIfNotOnUIThread();

        var textView = Package.GetActiveTextView();

        return textView.TryGetActiveTypeSymbol(out _activeTypeSymbol);
    }
}
