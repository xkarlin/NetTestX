using System;
using Community.VisualStudio.Toolkit;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.Shell;
using NetTestX.VSIX.Commands.Handlers;
using NetTestX.VSIX.Commands.Helpers;
using NetTestX.VSIX.Extensions;

namespace NetTestX.VSIX.Commands;

[Command(PackageIds.GenerateTestsAdvancedEditorCommand)]
internal sealed class GenerateTestsAdvancedEditorCommand : BaseCommand<GenerateTestsAdvancedEditorCommand, GenerateTestsAdvancedEditorCommandHandler>
{
    private INamedTypeSymbol _activeTypeSymbol;

    protected override GenerateTestsAdvancedEditorCommandHandler CreateHandler() => new(DTE, _activeTypeSymbol);

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

        return textView.TryGetActiveTypeSymbol(out _activeTypeSymbol) && SymbolHelper.CanGenerateTestsForTypeSymbol(_activeTypeSymbol);
    }
}
