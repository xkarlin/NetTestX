using System;
using System.IO;

namespace NetTestX.VSIX.Options.Validation;

/// <summary>
/// Validation for Windows file names
/// </summary>
public class TestFileNameValidation : Validation<string>
{
    public override bool Validate(string value) => value.IndexOfAny(Path.GetInvalidFileNameChars()) == -1;
}
