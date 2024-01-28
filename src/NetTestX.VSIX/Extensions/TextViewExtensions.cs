using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using Microsoft.VisualStudio.Text.Editor;

namespace NetTestX.VSIX.Extensions;

public static class TextViewExtensions
{
    public static bool TryGetActiveTypeSymbol(this ITextView textView, out INamedTypeSymbol typeSymbol)
    {
        var caretPosition = textView.Caret.Position.BufferPosition;

        var document = caretPosition.Snapshot.GetOpenDocumentInCurrentContextWithChanges();

        if (document is null || !document.TryGetSyntaxRoot(out var syntaxRoot) || !document.TryGetSemanticModel(out var semanticModel))
        {
            typeSymbol = null;
            return false;
        }

        var syntaxNode = syntaxRoot.FindToken(caretPosition).Parent;

        if (syntaxNode is not TypeDeclarationSyntax typeDeclarationSyntax)
        {
            typeSymbol = null;
            return false;
        }

        typeSymbol = semanticModel.GetDeclaredSymbol(typeDeclarationSyntax);
        return true;
    }
}
