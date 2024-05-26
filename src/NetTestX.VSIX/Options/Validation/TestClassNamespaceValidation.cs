using System;
using Microsoft.CodeAnalysis.CSharp;

namespace NetTestX.VSIX.Options.Validation;

/// <summary>
/// Validation for .NET namespaces
/// </summary>
public class TestClassNamespaceValidation : Validation<string>
{
    public override bool Validate(string value) => Array.TrueForAll(value.Split(['.']), SyntaxFacts.IsValidIdentifier);
}
