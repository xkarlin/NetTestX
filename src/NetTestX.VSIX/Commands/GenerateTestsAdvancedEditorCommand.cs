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

        var (visible, enabled) = ShouldCommandBeVisible();

        Command.Visible = visible;
        Command.Enabled = enabled;
    }

    private (bool Visible, bool Enabled) ShouldCommandBeVisible()
    {
        ThreadHelper.ThrowIfNotOnUIThread();

        var textView = Package.GetActiveTextView();

        if (!textView.TryGetActiveTypeSymbol(out _activeTypeSymbol))
            return (false, false);

        if (SymbolHelper.CanGenerateTestsForTypeSymbol(_activeTypeSymbol))
            return (true, true);

        return (true, false);
    }
}
