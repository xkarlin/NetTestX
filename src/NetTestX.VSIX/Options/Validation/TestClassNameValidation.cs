using Microsoft.CodeAnalysis.CSharp;

namespace NetTestX.VSIX.Options.Validation;

public class TestClassNameValidation : Validation<string>
{
    public override bool Validate(string value) => SyntaxFacts.IsValidIdentifier(value);
}
