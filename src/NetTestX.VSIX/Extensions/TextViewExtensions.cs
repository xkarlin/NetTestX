using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using Microsoft.VisualStudio.Text.Editor;

namespace NetTestX.VSIX.Extensions;

/// <summary>
/// Extensions for <see cref="ITextView"/>
/// </summary>
public static class TextViewExtensions
{
    /// <summary>
    /// Get an <see cref="INamedTypeSymbol"/> that is currently selected inside the <paramref name="textView"/>
    /// </summary>
    public static bool TryGetActiveTypeSymbol(this ITextView textView, out INamedTypeSymbol typeSymbol)
    {
        var caretPosition = textView.Caret.Position.BufferPosition;

        var document = caretPosition.Snapshot.GetOpenDocumentInCurrentContextWithChanges();

        if (document is null)
        {
            typeSymbol = null;
            return false;
        }

#pragma warning disable VSTHRD002 // Avoid problematic synchronous waits
#pragma warning disable VSTHRD104 // Offer async methods
        var syntaxRoot = document.GetSyntaxTreeAsync().Result;
#pragma warning restore VSTHRD104 // Offer async methods

        var semanticModel = document.GetSemanticModelAsync().Result;
#pragma warning restore VSTHRD002 // Avoid problematic synchronous waits

        var syntaxNode = syntaxRoot.GetRoot().FindToken(caretPosition).Parent;

        if (syntaxNode is not TypeDeclarationSyntax typeDeclarationSyntax)
        {
            typeSymbol = null;
            return false;
        }

        typeSymbol = semanticModel.GetDeclaredSymbol(typeDeclarationSyntax);
        return true;
    }
}
