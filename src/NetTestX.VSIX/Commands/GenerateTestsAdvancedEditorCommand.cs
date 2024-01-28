using System;
using System.Threading.Tasks;
using Community.VisualStudio.Toolkit;
using EnvDTE;
using EnvDTE80;
using Microsoft;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.Shell;
using NetTestX.VSIX.Commands.Handlers;
using NetTestX.VSIX.Extensions;

namespace NetTestX.VSIX.Commands;

[Command(PackageIds.GenerateTestsAdvancedEditorCommand)]
internal sealed class GenerateTestsAdvancedEditorCommand : BaseCommand<GenerateTestsAdvancedEditorCommand, GenerateTestsAdvancedEditorCommandHandler>
{
    private DTE2 _dte;

    private INamedTypeSymbol _activeTypeSymbol;

    protected override async Task InitializeCompletedAsync()
    {
        _dte = await Package.GetServiceAsync(typeof(DTE)) as DTE2;
        Assumes.Present(_dte);
    }

    protected override GenerateTestsAdvancedEditorCommandHandler CreateHandler() => new(_dte, _activeTypeSymbol);

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
