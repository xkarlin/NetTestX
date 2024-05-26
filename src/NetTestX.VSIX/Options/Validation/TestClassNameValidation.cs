using Microsoft.CodeAnalysis.CSharp;

namespace NetTestX.VSIX.Options.Validation;

/// <summary>
/// Validation for .NET class names
/// </summary>
public class TestClassNameValidation : Validation<string>
{
    public override bool Validate(string value) => SyntaxFacts.IsValidIdentifier(value);
}
